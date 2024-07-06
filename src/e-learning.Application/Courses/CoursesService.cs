using AutoMapper;
using e_learning.Application.Courses.DTOs;
using e_learning.Application.Services.Interfaces;
using e_learning.Domain.Entities;
using e_learning.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace e_learning.Application.Courses
{
    public class CoursesService : ICoursesService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public CoursesService(ICourseRepository courseRepository,
                             ICategoriesRepository categoriesRepository,
                             IFileService fileService,
                             IMapper mapper)
        {
            _courseRepository = courseRepository;
            _categoriesRepository = categoriesRepository;
            _fileService = fileService;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CourseWithInstructorDto>> TopCourses(int number)
        {
            var courses = await _courseRepository.GetTopCoursesWithInstructorsAsync(number);
            var coursesToReturn = _mapper.Map<IEnumerable<CourseWithInstructorDto>>(courses);
            return coursesToReturn;
        }
        public async Task<(IEnumerable<CourseDto>, int)> SearchCoursesAsync(CourseSearchDto searchParams)
        {
            try
            {
                var query = _courseRepository.GetQueryableCourses();

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
                var coursesDto = _mapper.Map<IEnumerable<CourseDto>>(pagedCourses);
                foreach (var courseDto in coursesDto)
                {
                    var course = pagedCourses.FirstOrDefault(c => c.Id == courseDto.Id);
                    if (course != null)
                    {
                        courseDto.Category = course.Category?.Name;
                        courseDto.Instructor = course.Instructor?.Name;
                    }
                }


                Console.WriteLine($"Found {pagedCourses.Count} courses");

                return (coursesDto, totalCourses);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while searching for courses: {ex.Message}");
                return (Enumerable.Empty<CourseDto>(), 0);
            }
        }


        public async Task<CourseDto> GetCourseByIdAsync(int id)
        {
            var course = await _courseRepository.GetCourseByIdAsync(id);

            if (course == null)
                return null; 

            var courseDto = _mapper.Map<CourseDto>(course);
            courseDto.Category = course.Category?.Name;
            courseDto.Instructor = course.Instructor?.Name;

            return courseDto;
        }

        public async Task<Course> CreateCourseAsync(CourseImgDto courseDto, int instructorId)
        {
            try
            {


                // Check if category exists
                var categoryExists = await _categoriesRepository.IfCatgoryExist(courseDto.CategoryId);
                if (!categoryExists)
                {
                    throw new ArgumentException("Category does not exist");
                }

                // Check if course name exists
                var courseExists = await _courseRepository.IfCourseExistsAsync(c => c.Name == courseDto.Name);
                if (courseExists)
                {
                    throw new ArgumentException("Course name already exists");
                }

                // Map DTO to entity using repository method
                var course = _mapper.Map<Course>(courseDto);

                // Save thumbnail using file service
                course.Thumbnail = await _fileService.SaveFileAsync(courseDto.Thumbnail);

                // Set additional properties
                course.InstructorId = instructorId;
                course.UploadDate = DateTime.UtcNow;

                // Add course using repository method
                var courseId = await _courseRepository.AddCourseAsync(course);

                // Return response based on whether course was added successfully
                return course;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("Failed to create course", ex);
            }
        }
    }
}
