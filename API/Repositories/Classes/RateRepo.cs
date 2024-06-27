using API.Context;
using API.Entities;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Classes
{
    public class RateRepo : IRateRepo
    {
        private readonly EcommerceContext _context;

        public RateRepo(EcommerceContext context)
        {
            _context = context;
        }
        public bool CourseExist(int courseId)
        {
            return _context.Courses.Any(p => p.Id == courseId);
        }
        public async Task RateAsync(int CourseId, int UserId, int NumOfStars)
        {
            var rate = await _context.Rates.Where(r => r.CourseId == CourseId && r.StudentId == UserId).FirstOrDefaultAsync();
            if (rate != null)
            {
                if (rate.Stars == NumOfStars)
                {
                    _context.Rates.Remove(rate);
                    await _context.SaveChangesAsync();
                    return;
                }
                _context.Rates.Remove(rate);
            }

            if (NumOfStars >= 1 && NumOfStars <= 5)
            {

                rate = new Rate
                {
                    StudentId = UserId,
                    CourseId = CourseId,
                    Stars = NumOfStars
                };

                 _context.Rates.Add(rate);
                await _context.SaveChangesAsync();


            }
        }

        public int GetRateForUser(int courseId, int studentId)
        {
            return _context.Rates.Where(r => r.CourseId == courseId && r.StudentId == studentId).Select(r => r.Stars).FirstOrDefault();
        }

        public int GetAvgRateForCourse(int studentId)
        {
            var ratings = _context.Rates
                        .Where(r => r.StudentId == studentId)
                        .Select(r => r.Stars)
                        .ToList();

            if (ratings.Any())
            {
                double averageRating = ratings.Average();
                return (int)Math.Round(averageRating);
            }
            else
            {
                return 0;
            }
        }

        public List<Rate> GetAllRatesForCourse(int studentId)
        {
            return _context.Rates.Where(r => r.StudentId == studentId).ToList();
        }
    }
}
