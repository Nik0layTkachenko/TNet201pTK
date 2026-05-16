using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartHome.Infrastructure.Models;

namespace SmartHome.Infrastructure.Data
{
    public class SmartHomeIdentityContext : IdentityDbContext<ApplicationUser>
    {
        public SmartHomeIdentityContext(DbContextOptions<SmartHomeIdentityContext> options) : base(options) { }

        // Example DbSets from previous labs could be added here
        // public DbSet<DeviceModel> Devices { get; set; }
    }
}