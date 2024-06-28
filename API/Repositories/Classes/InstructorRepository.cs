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

        public InstructorRepository(EcommerceContext context , IMapper mapper)
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
    }
}
