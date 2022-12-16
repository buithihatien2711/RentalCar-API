using System.ComponentModel.DataAnnotations;

namespace RentalCar.Model.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        [MaxLength(256)]
        public string? Fullname { get; set; }
        
        [Phone]
        [MaxLength(20)]
        public string? Contact { get; set; }
        
        [MaxLength(256)]
        public string? ProfileImage { get; set; }
        
        // [MaxLength(256)]
        // public string? CCCDImage { get; set; }
        
        [MaxLength(20)]
        public string? Gender { get; set; }
        
        public DateTime? DateOfBirth { get; set; }
        
        [MaxLength(20)]
        public string Username { get; set; }

        [EmailAddress]
        [MaxLength(256)]
        public string? Email { get; set; }
        
        public byte[] PasswordSalt { get; set; }
        
        public byte[] PasswordHash { get; set; }
        
        public DateTime? CreatedAt { get; set; }
        
        public DateTime? UpdateAt { get; set; }

        public List<RoleUser> RoleUsers {get; set; }
    
        public List<Car> Cars { get; set; }

        public License? License { get; set; }

        public List<Location>? Locations { get; set; }

        public List<CarReview>? CarReviews { get; set; }

        public double RatingLease { get; set; }

        public double RatingRent { get; set; }

        public List<Booking> Bookings { get; set; }

        public List<UserReviewUser>? UserReviewUsers { get; set; }

    }
}