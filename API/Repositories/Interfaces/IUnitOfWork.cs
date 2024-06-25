﻿namespace API.Repositories.Interfaces
{
        public interface IUnitOfWork
        {
                IUserRepo UserRepository { get; }

                ICourseRepository CourseRepository { get; }
                IInstructorRepository InstructorRepository { get; }

                IReviewRepo ReviewRepository { get; }
                IRateRepo RateRepository { get; }

                ISubscriptionRepository SubscriptionRepository { get; }
                ICoursePurchaseRepository CoursePurchaseRepository { get; }

                Task<bool> SaveChanges(); // for rollback if something filled in one of the transactions



                /* tell us if EF is tracking anything that's been changed inside its transaction.*/
                // to know if the context has changes that it is tracking.
                bool HasChanges();
        }
}
