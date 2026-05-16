using Microsoft.AspNetCore.Identity;
namespace SmartHome.Infrastructure.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string CustomProperty { get; set; } // Example of extending
    }
}