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

        public DbSet<CarReview> CarReviews { get; set; }

        public DbSet<Transmission> Transmissions { get; set; }

        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<CarRegister> CarRegisters { get; set; }
        public DbSet<CarTypeRegister> CarTypeRegisters { get; set; }
        public DbSet<CarImgRegister> CarImgRegisters { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<CarSchedule> CarSchedules { get; set; }

        public DbSet<UserReview> UserReviews { get; set; }
        
        public DbSet<UserReviewUser> UserReviewUsers { get; set; }
        public DbSet<AdvertisingPhoto> AdvertisingPhotos { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleUser>()
                .HasKey(ru => new { ru.UserId, ru.RoleId });
            
            modelBuilder.Entity<UserReviewUser>()
                .HasKey(uru => new { uru.UserId, uru.UserReviewId });

            // modelBuilder.Entity<CarRegister>()
            //     .HasKey(ru => new { ru.CarId, ru.CarTypeRgtId});
            
            modelBuilder.Entity<CarImgRegister>()
                        .HasOne(r => r.CarRegister)
                        .WithMany(c => c.CarImgRegisters)
                        .HasForeignKey(r => r.CarRegisterId)
                        .OnDelete(DeleteBehavior.ClientCascade);


            modelBuilder.Entity<Car>()
                        .HasOne(c => c.User)
                        .WithMany(u => u.Cars)
                        .HasForeignKey(c => c.UserId)
                        .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<CarReview>()
                        .HasOne(r => r.Car)
                        .WithMany(c => c.CarReviews)
                        .HasForeignKey(r => r.CarId)
                        .OnDelete(DeleteBehavior.ClientCascade);


            modelBuilder.Entity<Booking>()
                        .HasOne(r => r.Location)
                        .WithMany(u => u.Bookings)
                        .HasForeignKey(r => r.LocationId)
                        .OnDelete(DeleteBehavior.ClientCascade);

             modelBuilder.Entity<Booking>()
                        .HasOne(r => r.User)
                        .WithMany(u => u.Bookings)
                        .HasForeignKey(r => r.UserId)
                        .OnDelete(DeleteBehavior.ClientCascade);

            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        ////string connectionString = "Server=serverhaiyen.mysql.database.azure.com; Port=3306; Database=RentalCarDatabase; Uid=haiyen@serverhaiyen; Pwd=#Hthy01042001; SslMode=Preferred;";
        ////var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
        //////var serverVersion = new MySqlServerVersion(new Version(5, 7, 0));
        ////optionsBuilder
        ////    .UseMySql(connectionString, serverVersion)
        ////    .LogTo(Console.WriteLine, LogLevel.Information)
        ////    .EnableSensitiveDataLogging()
        ////    .EnableDetailedErrors();

        //    string connectionString = "Server=localhost; Database=RentalCar;Trusted_Connection=True;User ID=sa; Password=01042001";
        //    optionsBuilder.UseSqlServer(connectionString);
        //}
    }
}