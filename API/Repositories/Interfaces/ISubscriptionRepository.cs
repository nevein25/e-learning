using API.Controllers;
using API.Entities;

namespace API.Repositories.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task<Subscriber> UpdateAsync(Subscriber subscription);
        Task<IEnumerable<Subscriber>> GetAsync();
        Task<Subscriber> GetByIdAsync(string id);
        Task<Subscriber> GetByCustomerIdAsync(string id);
        Task<Subscriber> CreateAsync(Subscriber subscription);
        Task DeleteAsync(Subscriber subscription);
        Task CreateCoursePurchaseAsync(CoursePurchase coursePurchase);
        Task<Course> GetCourseById(int id);
    }
}
