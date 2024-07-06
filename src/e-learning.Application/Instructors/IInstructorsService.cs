using e_learning.Application.Instructors.DTOs;
using Microsoft.AspNetCore.Http;

namespace e_learning.Application.Instructors
{
    public interface IInstructorsService
    {
        Task<IEnumerable<InstructorDto>> GetInstructors();
        Task<InstructorDto> GetInstructorByIdAsync(int id);
        Task<IEnumerable<InstructorDto>> GetTopInstructorAsync(int number);
        Task<bool> DeletePaper(string username);
        Task<bool> AddPhoto(IFormFile file, string username);
        Task<object> IsInstructorVerified(int instructorId);
        Task<IEnumerable<InstructorApplicationDto>> GetInstrctorsApplicationsAsync();
        Task<InstructorApplicationDto> GetInstrctorsApplicationByIdAsync(int instructorId);
        Task VerifyInstructorById(int instructorId);
    }
}
