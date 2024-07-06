using System.Security.Claims;


namespace e_learning.API.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name)?.Value;
        }

        public static int GetUserId(this ClaimsPrincipal user)
        {
            try
            {
                return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            }
            catch
            {
                return 0;
            }
        }
        
        public static int GetUserEmail(this ClaimsPrincipal user)
        {
            return int.Parse(user.FindFirst(ClaimTypes.Email)?.Value);
        }

    }
}
