using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyCoursesController : ControllerBase
    {
        //    //  stripe listen --forward-to https://localhost:7154/api/buycourses/webhook

        private readonly StripeSettings _stripeSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;

        public BuyCoursesController(IOptions<StripeSettings> stripeSettings,
                                  IUnitOfWork unitOfWork,
                                  UserManager<AppUser> userManager)
        {
            _stripeSettings = stripeSettings.Value;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpPost("create-checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] CreateCheckoutSessionRequest req)
        {
            try
            {
                var testid = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                Console.WriteLine(testid);
                var course = await _unitOfWork.SubscriptionRepository.GetCourseById(int.Parse(req.CourseId)); // Fetch course details

                var options = new SessionCreateOptions
                {
                    SuccessUrl = req.SuccessUrl,
                    CancelUrl = req.FailureUrl,
                    
                    PaymentMethodTypes = new List<string>
                    {
                        "card",
                    },
                    Mode = "payment",
                    LineItems = new List<SessionLineItemOptions>
                    {
                        new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                Currency = "usd",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = course.Name
                                },
                                UnitAmount = (long)(course.Price * 100), // Convert to cents
                            },
                            Quantity = 1,
                        },
                    },
                    Metadata = new Dictionary<string, string>
                    {

                        { "courseId", course.Id.ToString() }, // Example: Course ID
                        { "userId", User.GetUserId().ToString() } // Example: User ID (if using JWT)
                    }
                };

                var service = new SessionService();
                var session = await service.CreateAsync(options);

                return Ok(new CreateCheckoutSessionResponse
                {
                    SessionId = session.Id,
                    PublicKey = _stripeSettings.PublicKey
                });
            }
            catch (StripeException e)
            {
                Console.WriteLine(e.StripeError.Message);
                return BadRequest(new ErrorResponse
                {
                    ErrorMessage = new ErrorMessage
                    {
                        Message = e.StripeError.Message,
                    }
                });
            }
        }

        [HttpPost("customer-portal")]
        public async Task<IActionResult> CustomerPortal([FromBody] CustomerPortalRequest req)
        {
            try
            {
                var options = new Stripe.BillingPortal.SessionCreateOptions
                {
                    Customer = _unitOfWork.UserRepository.GetCustomerIdByUserId(User.GetUserId()),
                    ReturnUrl = req.ReturnUrl,
                };
                var service = new Stripe.BillingPortal.SessionService();
                var session = await service.CreateAsync(options);

                return Ok(new
                {
                    url = session.Url
                });
            }
            catch (StripeException e)
            {
                Console.WriteLine(e.StripeError.Message);
                return BadRequest(new ErrorResponse
                {
                    ErrorMessage = new ErrorMessage
                    {
                        Message = e.StripeError.Message,
                    }
                });
            }
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                 json,
                 Request.Headers["Stripe-Signature"],
                 _stripeSettings.WHSecret
               );

                // Handle the event
                if (stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                    var session = stripeEvent.Data.Object as Session;

                    // Process the completed checkout session
                    await ProcessCompletedCheckout(session);
                }
                else if (stripeEvent.Type == Events.CustomerCreated)
                {
                    var customer = stripeEvent.Data.Object as Customer;

                    // Handle customer creation
                    await HandleCustomerCreation(customer);
                }
                else
                {
                    // Unexpected event type
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }
                return Ok();
            }
            catch (StripeException e)
            {
                Console.WriteLine(e.StripeError.Message);
                return BadRequest();
            }
        }

        private async Task ProcessCompletedCheckout(Session session)
        {
            try
            {
                // Retrieve course information from session metadata or API
                var courseId = session.Metadata["courseId"];
                var course = await _unitOfWork.SubscriptionRepository.GetCourseById(int.Parse(courseId));

                // Optionally, mark the course as purchased for the user
                var userId = session.Metadata["userId"];
                await MarkCourseAsPurchased(userId, courseId);

                Console.WriteLine("Course purchased successfully: " + course.Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to process completed checkout session: " + ex.Message);
            }
        }

        private async Task MarkCourseAsPurchased(string userId, string courseId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    CoursePurchase coursePurchase = new CoursePurchase
                    {
                        CourseId = courseId,
                        UserId = int.Parse(userId),
                        CustomerId = userId,
                        PurchaseDate = DateTime.Now
                    };
                    await _unitOfWork.SubscriptionRepository.CreateCoursePurchaseAsync(coursePurchase);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to mark course as purchased: " + ex.Message);
            }
        }

        private async Task HandleCustomerCreation(Customer customer)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(customer.Email);
                if (user != null)
                {
                    user.CustomerId = customer.Id;
                    await _userManager.UpdateAsync(user);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to handle customer creation: " + ex.Message);
            }
        }
    }
}
