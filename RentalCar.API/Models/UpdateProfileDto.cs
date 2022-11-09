namespace RentalCar.API.Models
{
    public class UpdateProfileDto
    {
        public string Fullname { get; set; }

        public string Contact { get; set; }

        public string ProfileImage { get; set; }
        
        public string Gender { get; set; }
        
        public DateTime DateOfBirth { get; set; }

        public LicenseDto LicenseDto { get; set; }
    }
}
