
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
    }
}