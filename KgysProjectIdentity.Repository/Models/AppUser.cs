using Microsoft.AspNetCore.Identity;

namespace KgysProjectIdentity.Repository.Models
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? Picture { get; set; }
    }
}
