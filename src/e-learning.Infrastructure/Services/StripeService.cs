using e_learning.Application.Payments.DTOs;
using e_learning.Domain.Entities;
using e_learning.Domain.Repositories;
using e_learning.Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace e_learning.Infrastructure.Services
{
    internal class StripeService : IStripeService
    {
        private readonly StripeSettings _stripeSettings;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICourseRepository _courseRepository;
        private readonly ICoursesPurshasedRepository _coursesPurshasedRepository;

        public StripeService(IOptions<StripeSettings> stripeSettings,
                             UserManager<AppUser> userManager,
                             ICourseRepository courseRepository,
                             ICoursesPurshasedRepository coursesPurshasedRepository)
        {
            _stripeSettings = stripeSettings.Value;
            _userManager = userManager;
            _courseRepository = courseRepository;
            _coursesPurshasedRepository = coursesPurshasedRepository;
        }

        public async Task<CreateCheckoutSessionResponse> CreateCheckoutSessionAsync(CreateCheckoutSessionRequest req, string userId)
        {
            var course = await _courseRepository.GetCourseByIdAsync(int.Parse(req.CourseId));

            var options = new SessionCreateOptions
            {
                SuccessUrl = req.SuccessUrl,
                CancelUrl = req.FailureUrl,
                PaymentMethodTypes = new List<string> { "card" },
                Mode = "payment",
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions { Name = course.Name },
                            UnitAmount = (long)(course.Price * 100), // Convert to cents
                        },
                        Quantity = 1,
                    }
                },
                Metadata = new Dictionary<string, string>
                {
                    { "courseId", course.Id.ToString() },
                    { "userId", userId }
                }
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return new CreateCheckoutSessionResponse
            {
                SessionId = session.Id,
                PublicKey = _stripeSettings.PublicKey
            };
        }

        public async Task<string> CreateCustomerPortalSessionAsync(string customerId, string returnUrl)
        {
            var options = new SessionCreateOptions
            {
                Customer = customerId,
                ReturnUrl = returnUrl,
            };
            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return session.Url;
        }

        public async Task HandleWebhookAsync(Stream body, string signatureHeader)
        {
            var json = await new StreamReader(body).ReadToEndAsync();
            var stripeEvent = EventUtility.ConstructEvent(json, signatureHeader, _stripeSettings.WHSecret);

            switch (stripeEvent.Type)
            {
                case Events.CheckoutSessionCompleted:
                    var session = stripeEvent.Data.Object as Session;
                    await ProcessCompletedCheckout(session);
                    break;

                case Events.CustomerCreated:
                    var customer = stripeEvent.Data.Object as Customer;
                    await HandleCustomerCreation(customer);
                    break;

                default:
                    Console.WriteLine($"Unhandled event type: {stripeEvent.Type}");
                    break;
            }
        }

        private async Task ProcessCompletedCheckout(Session session)
        {
            var courseId = session.Metadata["courseId"];
            var course = await _courseRepository.GetCourseByIdAsync(int.Parse(courseId));

            var userId = session.Metadata["userId"];
            await MarkCourseAsPurchased(userId, courseId);

            Console.WriteLine($"Course purchased successfully: {course.Name}");
        }

        private async Task MarkCourseAsPurchased(string userId, string courseId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var coursePurchase = new CoursePurchase
                {
                    CourseId = courseId,
                    UserId = int.Parse(userId),
                    CustomerId = userId,
                    PurchaseDate = DateTime.Now
                };
                await _coursesPurshasedRepository.CreateCoursePurchaseAsync(coursePurchase);
            }
        }

        private async Task HandleCustomerCreation(Customer customer)
        {
            var user = await _userManager.FindByEmailAsync(customer.Email);
            if (user != null)
            {
                user.CustomerId = customer.Id;
                await _userManager.UpdateAsync(user);
            }
        }
    }
}
