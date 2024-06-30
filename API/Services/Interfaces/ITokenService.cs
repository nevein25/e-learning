using API.Entities;

namespace API.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user, DateTime? expDateOfSubscription, bool isSubscribed, bool isInstructorVerified);
    }
}
