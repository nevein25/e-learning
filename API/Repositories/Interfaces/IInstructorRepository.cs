
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Repositories.Interfaces
{
    public interface IInstructorRepository
    {
        Task AddInstructorAsync(Instructor instructor);
        Task DeleteInstructorAsync(int id);
        Task<IEnumerable<InstructorDto>> GetInstructorsAsync();
        Task<InstructorDto> GetInstructorByIdAsync(int id);
        Task UpdateInstructorAsync(Instructor instructor);

        Task<IEnumerable<InstructorDto>> GetTopFourInstructorAsync(int number);

        Task<Instructor> GetInstructorWithPhotoByUserNamesync(string username);
        Task<bool> IsVerified(int id);
        Task<int> GetInstructorIdByUsernameOrEmailAsync(string emailOrUsername);
        Task<IEnumerable<InstructorApplicationDto>> GetInstrctorsApplicationsAsync();
        Task VerifyInstructorById(int instructorId);
        Task<InstructorApplicationDto> GetInstrctorsApplicationByIdAsync(int instructorId);
    }
}