namespace API.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepo UserRepository { get; }
<<<<<<< HEAD
        ICourseRepository CourseRepository { get; }
        IInstructorRepository InstructorRepository { get; }


        Task<bool> SaveChanges(); // for rollback if something filled in one of the transactions
=======
        IReviewRepo ReviewRepository { get; }
        IRateRepo RateRepository { get; }
        Task<bool> SaveChanges(); // for rollback if someting filled in one of the transactions
>>>>>>> rate-review

        /* tell us if EF is tracking anything that's been changed inside its transaction.*/
        // to know if the context has changes that it is tracking.
        bool HasChanges();
    }
}
