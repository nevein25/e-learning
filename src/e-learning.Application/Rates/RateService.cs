using AutoMapper;
using e_learning.Application.Rates.DTOs;
using e_learning.Domain.Entities;
using e_learning.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace e_learning.Application.Rates
{
    internal class RateService : IRateService
    {
        private readonly IRatesRepository _rateRepository;
        private readonly IMapper _mapper;

        public RateService(IRatesRepository rateRepository, IMapper mapper)
        {
            _rateRepository = rateRepository;
            _mapper = mapper;
        }


        public async Task RateAsync(int CourseId, int UserId, int NumOfStars)
        {
            if (!_rateRepository.IsCourseExist(CourseId)) throw new Exception("Course not found");


            var rate = await _rateRepository.GetRateAsync(CourseId, UserId);
          
            if (rate != null)
            {
                if (rate.Stars == NumOfStars)
                {
                    await _rateRepository.DeleteRateAsync(rate);
                    return;
                }
                await _rateRepository.DeleteRateAsync(rate);
            }

            if (NumOfStars >= 1 && NumOfStars <= 5)
            {

                rate = new Rate
                {
                    StudentId = UserId,
                    CourseId = CourseId,
                    Stars = NumOfStars
                };

                await _rateRepository.AddRateAsync(rate);


            }
        }

        //public async Task<RateByUserDto> GetRateForStudentAsync(int courseId, int studentId)
        //{
        //    var stars = await _context.Rates
        //        .Where(r => r.CourseId == courseId && r.StudentId == studentId)
        //        .Select(r => r.Stars).FirstOrDefaultAsync();
        //    RateByUserDto rateByUser = new()
        //    {
        //        Stars = stars
        //    };
        //    return rateByUser;
        //}

        public async Task<int> GetAvgRateForCourseAsync(int courseId)
        {
            var ratings = await _rateRepository.GetAllRatesByCourseIdAsync(courseId);

            if (ratings.Any())
            {
                double averageRating = ratings.Average(r => r.Stars);
                return (int)Math.Round(averageRating);
            }
            else
            {
                return 0;
            }
        }

        //public List<Rate> GetAllRatesForCourse(int studentId)
        //{
        //    return _context.Rates.Where(r => r.StudentId == studentId).ToList();
        //}

        public async Task<RateByUserDto> GetRateForLogedinStudent(int courseId, int studentId)
        {
            if (!_rateRepository.IsCourseExist(courseId)) throw new Exception("Course not found");

            var rate = await _rateRepository.GetRateAsync(courseId, studentId);
            var rateDto = _mapper.Map<RateByUserDto>(rate);

            return rateDto;
        }
    }
}
