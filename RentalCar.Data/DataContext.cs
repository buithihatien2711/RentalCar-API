using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RentalCar.Model.Models;

namespace RentalCar.Data
{
    public class DataContext : DbContext
    {
        public DataContext(){}
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        

        public DbSet<CarModel> CarModels { get; set; }

        public DbSet<District> Districts { get; set; }

        public DbSet<Ward> Wards { get; set; }
        public DbSet<CarBrand> CarBrands { get; set; }
        
        public DbSet<Location> Locations { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<RoleUser> RoleUsers { get; set; }

        public DbSet<CarImage> CarImages { get; set; }

        public DbSet<License> Licenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleUser>()
                .HasKey(ru => new { ru.UserId, ru.RoleId });
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "server=localhost;database=RentalCar;user=root;";
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            optionsBuilder
                .UseMySql(connectionString, serverVersion)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();

        }
    }
}