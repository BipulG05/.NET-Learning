using WebApi2024.Models;

namespace WebApi2024.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
