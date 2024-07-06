using e_learning.Application.Certificates.DTOS;
using e_learning.Domain.Entities;
using e_learning.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace e_learning.Application.Certificates
{
    internal class CertifcatesService : ICertifcatesService
    {
        private readonly ICoursesPurshasedRepository _coursesPurshasedRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly UserManager<AppUser> _userManager;

        public CertifcatesService(ICoursesPurshasedRepository coursesPurshasedRepository,
                                  ICourseRepository courseRepository,
                                  UserManager<AppUser> userManager)
        {
           _coursesPurshasedRepository = coursesPurshasedRepository;
           _courseRepository = courseRepository;
           _userManager = userManager;
        }
        public async Task<CertificateDto> GetCertificateData(int courseId, int studentId)
        {
            var studentName = (await _userManager.FindByIdAsync(studentId.ToString())).Name;

            var courseName = (await _courseRepository.GetCourseByIdAsync(courseId)).Name;

            var finishedDate = (await _coursesPurshasedRepository.GetCoursePurchaseAsync(studentId, courseId)).FinishedDate;

            var certificate = new CertificateDto
            {
                CertificateId = "",
                CompletionDate = DateTime.UtcNow,
                StudentName = studentName,
                CourseName = courseName,
                FinishedDate = finishedDate
            };
            return certificate;
        }
    }
}
