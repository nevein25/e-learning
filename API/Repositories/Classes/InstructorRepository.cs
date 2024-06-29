using API.Context;
using API.DTOs;
using API.Entities;
using API.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace API.Repositories.Classes
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly EcommerceContext _context;
        private readonly IMapper _mapper;

        public InstructorRepository(EcommerceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InstructorDto>> GetInstructorsAsync()
        {
            var instructors = await _context.Instructors.ToListAsync();
            var instructorsDto = _mapper.Map<IEnumerable<InstructorDto>>(instructors);
            return instructorsDto;

        }

        public async Task<InstructorDto> GetInstructorByIdAsync(int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            var instructorDto = _mapper.Map<InstructorDto>(instructor);
            return instructorDto;

        }

        public async Task AddInstructorAsync(Instructor instructor)
        {
            _context.Instructors.Add(instructor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateInstructorAsync(Instructor instructor)
        {
            _context.Entry(instructor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInstructorAsync(int id)
        {
            var instructorToDelete = await _context.Instructors.FindAsync(id);
            _context.Instructors.Remove(instructorToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<InstructorDto>> GetTopFourInstructorAsync(int number)
        {
            var instructors = await _context.Instructors.Take(number).ToListAsync();
            var instructorDto = _mapper.Map<IEnumerable<InstructorDto>>(instructors);
            return instructorDto;

        }

        public async Task<Instructor> GetInstructorWithPhotoByUserNamesync(string username)
        {
            return await _context.Instructors.Where(i => i.UserName == username).FirstOrDefaultAsync();
        }

        public async Task<bool> IsVerified(int id)
        {
            return await _context.Instructors.Where(i => i.Id == id).Select(i => i.IsVerified).FirstOrDefaultAsync();
        }

        public async Task<int> GetInstructorIdByUsernameOrEmailAsync(string emailOrUsername)
        {
            return await _context.Instructors.Where(i => i.Email == emailOrUsername || i.UserName == emailOrUsername)
                .Select(i => i.Id).FirstOrDefaultAsync();
        }

        public async Task<string> GetInstructorEmailByIdAsync(int instructorId)
        {
            return await _context.Instructors.Where(i => i.Id == instructorId)
                .Select(i => i.Email).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<InstructorApplicationDto>> GetInstrctorsApplicationsAsync()
        {
            var instructors = await _context.Instructors.Where(i => i.IsVerified == false).ToListAsync();
            var instructorDtos = instructors.Select(instructor => new InstructorApplicationDto
            {
                InstructorId = instructor.Id,
                Username = instructor.UserName,
                Paper = instructor.Paper
            }).ToList();

            return instructorDtos;
        }

        public async Task<InstructorApplicationDto> GetInstrctorsApplicationByIdAsync(int instructorId)
        {
            var instructor = await _context.Instructors.Where(i => i.Id == instructorId).FirstOrDefaultAsync();
            var instructorDto = new InstructorApplicationDto
            {
                InstructorId = instructor.Id,
                Username = instructor.UserName,
                Paper = instructor.Paper
            };

            return instructorDto;
        }
        public async Task VerifyInstructorById(int instructorId)
        {
            var instructor = await _context.Instructors.Where(i => i.Id == instructorId).FirstOrDefaultAsync();
            instructor.IsVerified = true;
            await _context.SaveChangesAsync();
        }
    }
}
