namespace RentalCar.Model.Models
{
    public class AdvertisingPhoto
    {
        public int Id { get; set; }
        public string? Path { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }

    }
}