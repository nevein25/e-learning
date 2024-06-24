using API.Context;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace API.Controllers
{
    // // i think im gonna delete the whole controller
    [Route("api/[controller]")]
    [ApiController]
    //    //  stripe listen --forward-to https://localhost:7154/api/payments/webhook

    public class PaymentsController : ControllerBase
    {

        private readonly StripeSettings _stripeSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;

        public PaymentsController(IOptions<StripeSettings> stripeSettings, 
                                  IUnitOfWork unitOfWork,
                                  UserManager<AppUser> userManager)
        {
            // StripeConfiguration.ApiKey = "sk_test_51PV8p82MXG6xva1odV6I3wyAURHgwn35floMEbpJ24xlsKy2i10359H4nza3hcqVw8yRCjOXXa8I4KfVxUf2yAzh0017nRIAeN";
            _stripeSettings = stripeSettings.Value;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        // for testing
        [HttpGet("products")]
        public IActionResult Products()
        {

            //StripeConfiguration.ApiKey = "sk_test_51PV8p82MXG6xva1odV6I3wyAURHgwn35floMEbpJ24xlsKy2i10359H4nza3hcqVw8yRCjOXXa8I4KfVxUf2yAzh0017nRIAeN";

            var options = new ProductListOptions
            {
                Limit = 3,
            };
            var service = new ProductService();
            StripeList<Product> products = service.List(
              options
            );


            return Ok(products);
        }

        [HttpPost("create-checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] CreateCheckoutSessionRequest req)
        {
            var options = new SessionCreateOptions
            {
                SuccessUrl = req.SuccessUrl,
                CancelUrl = req.FailureUrl,
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                Mode = "subscription",
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Price = req.PriceId,
                        Quantity = 1,
                    },
                },
            };

            var service = new SessionService();
            service.Create(options);
            try
            {
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

        [Authorize]
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
                if (stripeEvent.Type == Events.CustomerSubscriptionCreated)
                {
                    var subscription = stripeEvent.Data.Object as Subscription;
                    //Do stuff
                    await addSubscriptionToDb(subscription);
                }
                else if (stripeEvent.Type == Events.CustomerSubscriptionUpdated)
                {
                    var session = stripeEvent.Data.Object as Stripe.Subscription;

                    // Update Subsription
                    await updateSubscription(session);
                }
                else if (stripeEvent.Type == Events.CustomerCreated)
                {
                    var customer = stripeEvent.Data.Object as Customer;
                    //Do Stuff
                    await addCustomerIdToUser(customer);
                }
                // ... handle other event types
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

        private async Task updateSubscription(Subscription subscription)
        {
            try
            {
                var subscriptionFromDb = await _unitOfWork.SubscriptionRepository.GetByIdAsync(subscription.Id);
                if (subscriptionFromDb != null)
                {
                    subscriptionFromDb.Status = subscription.Status;
                    subscriptionFromDb.CurrentPeriodEnd = subscription.CurrentPeriodEnd;
                    await _unitOfWork.SubscriptionRepository.UpdateAsync(subscriptionFromDb);
                    Console.WriteLine("Subscription Updated");
                }

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);

                Console.WriteLine("Unable to update subscription");

            }

        }

        private async Task addCustomerIdToUser(Customer customer)
        {
            try
            {
                var userFromDb = await _userManager.FindByEmailAsync(customer.Email);
               
                if (userFromDb != null)
                {
                    userFromDb.CustomerId = customer.Id;
                    await _userManager.UpdateAsync(userFromDb);
                    Console.WriteLine("Customer Id added to user ");
                }

            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Unable to add customer id to user");
                Console.WriteLine(ex);
            }
        }

        private async Task addSubscriptionToDb(Subscription subscription)
        {
            try
            {
                var subscriber = new Subscriber
                {
                    Id = subscription.Id,
                    CustomerId = subscription.CustomerId,
                    Status = "active",
                    CurrentPeriodEnd = subscription.CurrentPeriodEnd
                };
                await _unitOfWork.SubscriptionRepository.CreateAsync(subscriber);

                //You can send the new subscriber an email welcoming the new subscriber
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Unable to add new subscriber to Database");
                Console.WriteLine(ex.Message);
            }
        }
    }


}