using API.DTOs;

namespace API.Repositories.Interfaces
{
    public interface ICoursePurchaseRepository
    {
        Task<IList<CoursesPurshasedDto>> CoursesBoughtByStudentId(int studentId);
    }
}
