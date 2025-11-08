using Microsoft.AspNetCore.Identity;

namespace WebApi2024.Models
{
    public class AppUser :IdentityUser
    {
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio> ();
    }
}
