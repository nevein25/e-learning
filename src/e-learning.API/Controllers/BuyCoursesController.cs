using e_learning.API.Extensions;
using e_learning.Application.Payments.DTOs;
using e_learning.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyCoursesController : ControllerBase
    {
        //    //  stripe listen --forward-to https://localhost:7154/api/buycourses/webhook

        private readonly IStripeService _stripeService;

        public BuyCoursesController(IStripeService stripeService)
        {
            _stripeService = stripeService;
        }

        [HttpPost("create-checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] CreateCheckoutSessionRequest req)
        {
            var userId = User.GetUserId();
            if (userId == null)
                return Unauthorized();

            var response = await _stripeService.CreateCheckoutSessionAsync(req, userId.ToString());
            return Ok(response);
        }

        [HttpPost("customer-portal")]
        public async Task<IActionResult> CustomerPortal([FromBody] CustomerPortalRequest req)
        {
            var userId = User.GetUserId();
            if (userId == null)
                return Unauthorized();

            var portalUrl = await _stripeService.CreateCustomerPortalSessionAsync(userId.ToString(), req.ReturnUrl);

            return Ok(new { url = portalUrl });
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> WebHook()
        {
            var signatureHeader = Request.Headers["Stripe-Signature"];
            await _stripeService.HandleWebhookAsync(Request.Body, signatureHeader);

            return Ok();
        }
    }
}
