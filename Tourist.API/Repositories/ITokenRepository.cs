using Microsoft.AspNetCore.Identity;

namespace Tourist.API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
