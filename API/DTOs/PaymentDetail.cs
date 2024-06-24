using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    // I need all
    public class CreateCheckoutSessionRequest
    {
        [Required]

        public string? CourseId { get; set; } /////
        [Required]

        public string? PriceId { get; set; }
        [Required]
        public string? SuccessUrl { get; set; }
        [Required]
        public string? FailureUrl { get; set; }
    }
    public class CreateCheckoutSessionResponse
    {
        public string? SessionId { get; set; }
        public string? PublicKey { get; set; }

    }
    public class CustomerPortalRequest
    {
        [Required]
        public string? ReturnUrl { get; set; }
    }
    public class ErrorMessage
    {
        public string Message { get; set; }
    }
    public class ErrorResponse
    {
        public ErrorMessage ErrorMessage { get; set; }
    }
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class StripeSettings
    {
        public string? PublicKey { get; set; }
        public string? WHSecret { get; set; }
    }

}
