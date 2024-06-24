using API.Context;
using API.Controllers;
using API.Entities;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Classes
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly EcommerceContext _context;

        public SubscriptionRepository(EcommerceContext context)
        {
            _context = context;
        }
        public async Task<Subscriber> CreateAsync(Subscriber subscription)
        {
            await _context.Subscribers.AddAsync(subscription);
            await _context.SaveChangesAsync();
            return subscription;
        }

        public async Task DeleteAsync(Subscriber subscription)
        {
            _context.Subscribers.Remove(subscription);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Subscriber>> GetAsync()
        {
            return await _context.Subscribers.ToListAsync();
        }

        public async Task<Subscriber> GetByCustomerIdAsync(string id)
        {
            return await _context.Subscribers.SingleOrDefaultAsync(x => x.CustomerId == id);
        }

        public async Task<Subscriber> GetByIdAsync(string id)
        {
            return await _context.Subscribers.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Subscriber> UpdateAsync(Subscriber subscription)
        {
            _context.Subscribers.UpdateRange(subscription);
            await _context.SaveChangesAsync();
            return subscription;
        }

        public async Task CreateCoursePurchaseAsync(CoursePurchase coursePurchase)
        {
            _context.CoursePurchases.Add(coursePurchase);
            await _context.SaveChangesAsync();
        }

        public async Task<Course> GetCourseById(int id)
        {
            var course = await _context.Courses.Where(c => c.Id == id).FirstOrDefaultAsync();
            return course;
        }
     }
}