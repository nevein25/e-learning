namespace e_learning.Application.Payments.DTOs
{
    public class CreateCheckoutSessionRequest
    {
        public string CourseId { get; set; }
        public string SuccessUrl { get; set; }
        public string FailureUrl { get; set; }
    }
}
