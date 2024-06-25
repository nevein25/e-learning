﻿using API.Context;
using API.DTOs;
using API.Entities;
using API.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Classes
{
    public class CourseRepository : ICourseRepository
    {
        private readonly EcommerceContext _context;
        private readonly IMapper _mapper;

        public CourseRepository(EcommerceContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CourseDto> GetCourseById(int Id)
        {
            var course = _context.Courses.Where(c=>c.Id == Id).FirstOrDefault();
            
            var courseDto = _mapper.Map<CourseDto>(course);

            return courseDto;
        }

        public async Task<(IEnumerable<Course>, int)> SearchCoursesAsync(CourseSearchDto searchParams)
        {
            try
            {
                var query = _context.Courses.AsQueryable();

                Console.WriteLine($"Searching courses with parameters: Name={searchParams.Name}, MinPrice={searchParams.MinPrice}, MaxPrice={searchParams.MaxPrice}, CategoryId={searchParams.CategoryId}, PageNumber={searchParams.PageNumber}, PageSize={searchParams.PageSize}");

                if (!string.IsNullOrEmpty(searchParams.Name))
                {
                    query = query.Where(c => c.Name.Contains(searchParams.Name));
                }

                if (searchParams.MinPrice.HasValue)
                {
                    query = query.Where(c => c.Price >= searchParams.MinPrice.Value);
                }

                if (searchParams.MaxPrice.HasValue)
                {
                    query = query.Where(c => c.Price <= searchParams.MaxPrice.Value);
                }

                if (searchParams.CategoryId.HasValue)
                {
                    query = query.Where(c => c.CategoryId == searchParams.CategoryId.Value);
                }

                var totalCourses = await query.CountAsync();

                // Pagination
                var skip = (searchParams.PageNumber - 1) * searchParams.PageSize;
                var pagedCourses = await query.Skip(skip).Take(searchParams.PageSize).ToListAsync();

                Console.WriteLine($"Found {pagedCourses.Count} courses");

                return (pagedCourses, totalCourses);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while searching for courses: {ex.Message}");
                return (Enumerable.Empty<Course>(), 0);
            }
        }
    }
}