using API.Context;
using API.Repositories.Interfaces;
using AutoMapper;

namespace API.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EcommerceContext _context;
        private readonly IMapper _mapper;
        public UnitOfWork(EcommerceContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IUserRepo UserRepository => new UserRepo(_context);
        public ICatgoryRepository CatgoryRepository => new CatgoryRepository(_context);

        public ICourseRepository CourseRepository => new CourseRepository(_context , _mapper);
        public IInstructorRepository InstructorRepository => new InstructorRepository(_context , _mapper);


        public IReviewRepo ReviewRepository => new ReviewRepo(_context, _mapper);
        public IRateRepo RateRepository => new RateRepo(_context);
        public ISubscriptionRepository SubscriptionRepository => new SubscriptionRepository(_context);

        public ICoursePurchaseRepository CoursePurchaseRepository => new CoursePurchaseRepository(_context, _mapper);

        public IModuleRepositry ModuleRepositry => new ModuleRepository(_context, _mapper);

        public ILessonRepositry LessonRepositry => new LessonRepositry(_context, _mapper);

        public IEnrollmentRepository EnrollmentRepository => new EnrollmentRepository(_context, _mapper);

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
            // returns true. If ef is tracking any changes to our entities in memory.
        }
    }
}
