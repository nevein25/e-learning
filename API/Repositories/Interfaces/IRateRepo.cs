using API.Entities;

namespace API.Repositories.Interfaces
{
    public interface IRateRepo
    {
        void Rate(int StdId,int CourseId,int Stars);
    }
}
