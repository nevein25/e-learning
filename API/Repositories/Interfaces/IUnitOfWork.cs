namespace API.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepo UserRepository { get; }
        ISubscriptionRepository SubscriptionRepository { get; }
        Task<bool> SaveChanges(); // for rollback if someting filled in one of the transactions

        /* tell us if EF is tracking anything that's been changed inside its transaction.*/
        // to know if the context has changes that it is tracking.
        bool HasChanges();
    }
}
