using e_learning.Domain.Entities;

namespace e_learning.Application.Auth
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
