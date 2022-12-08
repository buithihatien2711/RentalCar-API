using System.ComponentModel.DataAnnotations;

namespace RentalCar.Model.Models
{
    public enum enumStatus
    {
        Confirm = 1,
        WaitConFirm = 2,
        Paid = 3

    }
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal Total { get; set; }
        public enumStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
  
    }
}