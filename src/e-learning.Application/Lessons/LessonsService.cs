using AutoMapper;
using e_learning.Application.Courses.DTOs;
using e_learning.Application.Lessons.DTOs;
using e_learning.Application.Modules.DTOs;
using e_learning.Domain.Common;
using e_learning.Domain.Entities;
using e_learning.Domain.Repositories;
using e_learning.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.Application.Lessons
{
    internal class LessonsService : ILessonsService
    {
        private readonly ILessonsRepository _lessonsRepository;
        private readonly IModulesRepositry _modulesRepositry;
        private readonly IVideoService _videoService;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public LessonsService(ILessonsRepository lessonsRepository, 
                              IModulesRepositry modulesRepositry,
                              IVideoService videoService,
                              IMapper mapper,
                              ICourseRepository courseRepository)
        {
            _lessonsRepository = lessonsRepository;
            _modulesRepositry = modulesRepositry;
            _videoService = videoService;
            _mapper = mapper;
            _courseRepository = courseRepository;
        }

        public async Task<int> GetLessonCountByCourseId(int id)
        {
          return await _lessonsRepository.GetLessonCountByCourseId(id);
         
        }

        public async Task<MediaUploadResult?> CreateLessonAsync(LessonDto lessonDto)
        {
            // Check Module Found Or Not
            var module = await _modulesRepositry.FindFirstModule(m => m.Id == lessonDto.ModuleId);
            if (module == null)
                return null; // or throw an exception

            // New Lesson Number Logic
            var lessons = await _lessonsRepository.FindLesson(lesson => lesson.ModuleId == lessonDto.ModuleId);
            var newLessonNumber = 1 + (lessons?.Count() ?? 0);
            var filePath = $"{module.Course.Name}/Chapter_{module.ModuleNumber}/Lesson_{newLessonNumber}";

            // Convert IFormFile to byte[]
            byte[] videoData;
            using (var memoryStream = new MemoryStream())
            {
                await lessonDto.VideoContent.CopyToAsync(memoryStream);
                videoData = memoryStream.ToArray();
            }

            // Upload To Cloudinary
            var uploadResult = await _videoService.UploadVideoAsync(videoData, filePath);
            if (uploadResult == null)
                return null; // or handle the error

            // Create Lesson 
            var lesson = _mapper.Map<Lesson>(lessonDto);
            lesson.LessonNumber = newLessonNumber;

            // Save To DB.
            return await _lessonsRepository.AddLesson(lesson) ? uploadResult : null;
        }


        public async Task<CourseContentDto> GetCourseWithLessonsContent(int courseId)
        {
            var courses = await _courseRepository.GetAllCoursesData();
            var courseContent = courses.FirstOrDefault(c => c.Id == courseId);

            if (courseContent == null) return null;

            var courseContentDto = new CourseContentDto
            {
                Name = courseContent.Name,
                Duration = courseContent.Duration,
                Description = courseContent.Description,
                Language = courseContent.Language,
                image = courseContent.Thumbnail,
                Modules = courseContent.Modules.Select(m => new ModuleContentDTO
                {
                    id = m.Id,
                    Name = m.Name,
                    ModuleNumber = m.ModuleNumber,
                    Lessons = m.Lessons.Select(l => new LessonContentDto
                    {
                        id = l.Id,
                        Name = l.Name,
                        Type = l.Type,
                        Content = l.Content,
                        LessonNumber = l.LessonNumber
                    }).ToList()
                }).ToList()
            };
            return courseContentDto;
        }
    }
}
