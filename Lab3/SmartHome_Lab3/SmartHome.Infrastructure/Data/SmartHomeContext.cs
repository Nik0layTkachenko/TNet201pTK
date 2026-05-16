using Microsoft.EntityFrameworkCore;
using SmartHome.Infrastructure.Models;

namespace SmartHome.Infrastructure.Data
{
    public class SmartHomeContext : DbContext
    {
        public DbSet<RoomModel> Rooms { get; set; }
        public DbSet<DeviceModel> Devices { get; set; }
        public DbSet<LightBulbModel> LightBulbs { get; set; }
        public DbSet<ThermostatModel> Thermostats { get; set; }
        public DbSet<BulbSettingsModel> BulbSettings { get; set; }
        public DbSet<UserGroupModel> UserGroups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=smarthome.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeviceModel>().ToTable("Devices");
            modelBuilder.Entity<LightBulbModel>().ToTable("LightBulbs");
            modelBuilder.Entity<ThermostatModel>().ToTable("Thermostats");

            modelBuilder.Entity<RoomModel>().HasMany(r => r.Devices).WithOne(d => d.Room).HasForeignKey(d => d.RoomId);
            modelBuilder.Entity<LightBulbModel>().HasOne(b => b.Settings).WithOne(s => s.LightBulb).HasForeignKey<BulbSettingsModel>(s => s.LightBulbId);
            modelBuilder.Entity<DeviceModel>().HasMany(d => d.UserGroups).WithMany(g => g.Devices).UsingEntity(j => j.ToTable("DeviceUserGroups"));
        }
    }
}