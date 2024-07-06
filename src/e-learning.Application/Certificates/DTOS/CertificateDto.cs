namespace e_learning.Application.Certificates.DTOS
{
    public class CertificateDto
    {
        public string StudentName { get; set; }
        public string CourseName { get; set; }
        public DateTime CompletionDate { get; set; }
        public string CertificateId { get; set; }
        public DateOnly? FinishedDate { get; set; }
    }
}
