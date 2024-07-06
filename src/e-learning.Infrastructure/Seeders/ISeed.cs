namespace e_learning.Infrastructure.Seeders
{
    public interface ISeed
    {
        Task SeedRoles();
        Task SeedCoursesWithDependenciesAsync();
    }
}
