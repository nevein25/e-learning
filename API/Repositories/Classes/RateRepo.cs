using API.Context;
using API.Entities;
using API.Repositories.Interfaces;

namespace API.Repositories.Classes
{
    public class RateRepo : IRateRepo
    {
        private readonly EcommerceContext _context;

        public RateRepo(EcommerceContext context)
        {
            _context = context;
        }
        public void Rate(int StdId, int CourseId, int Stars)
        {
            Rate rate = new();
            //if (Stars != 0){ }
                rate.Stars = Stars;
                rate.StudentId=StdId;
                rate.CourseId=CourseId;
                _context.Rates.Add(rate);
                _context.SaveChanges();
            
        }
    }
}
