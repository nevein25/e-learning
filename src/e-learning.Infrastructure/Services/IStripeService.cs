using e_learning.Application.Payments.DTOs;

namespace e_learning.Infrastructure.Services
{
    public interface IStripeService
    {
        Task<CreateCheckoutSessionResponse> CreateCheckoutSessionAsync(CreateCheckoutSessionRequest req, string userId);
        Task<string> CreateCustomerPortalSessionAsync(string customerId, string returnUrl);
        Task HandleWebhookAsync(Stream body, string signatureHeader);
    }
}
