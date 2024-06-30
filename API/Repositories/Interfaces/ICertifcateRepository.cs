using API.DTOs;

namespace API.Repositories.Interfaces
{
    public interface ICertifcateRepository
    {
        Task<CertificateDto> GetCertificateData(int courseId, int studentId);
    }
}
