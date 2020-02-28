using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppUser: IdentityUser
    {
        public string FullName { get; set; }
    }
}