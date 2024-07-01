using API.Context;
using API.DTOs;
using API.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Classes
{
    public class CertifcateRepository : ICertifcateRepository
    {
        private readonly EcommerceContext _context;
        private readonly IMapper _mapper;


        public CertifcateRepository(EcommerceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CertificateDto> GetCertificateData(int courseId, int studentId)
        {
            var studentName = await _context.Students
                .Where(s=> s.Id == studentId)
                .Select(s => s.Name)
                .FirstOrDefaultAsync();

            var courseName = await _context.Courses
                .Where(c => c.Id == courseId)
                .Select(c => c.Name)
                .FirstOrDefaultAsync();

            var finishedDate = await _context.CoursePurchases
                .Where(c => c.Id == courseId && c.UserId == studentId)
                .Select(c=> c.FinishedDate) 
                .FirstOrDefaultAsync();

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
