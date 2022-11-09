namespace RentalCar.API.Models
{
    public class UserProfile
    {
        public string Fullname { get; set; }

        public string Contact { get; set; }

        public string ProfileImage { get; set; }

        public string Gender { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public string Email { get; set; }

        public LicenseDto LicenseDto { get; set; }
    }
}

