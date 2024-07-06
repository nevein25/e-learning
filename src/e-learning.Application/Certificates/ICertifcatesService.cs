using e_learning.Application.Certificates.DTOS;

namespace e_learning.Application.Certificates
{
    public interface ICertifcatesService
    {
        Task<CertificateDto> GetCertificateData(int courseId, int studentId);
    }
}
