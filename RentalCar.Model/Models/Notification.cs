using System.ComponentModel.DataAnnotations;

namespace RentalCar.Model.Models
{
    public partial class Notification
    {
        public int Id { get; set; }
        public int FromUserId{ get; set; }
        public User? FromUser { get; set; }       
        public int ToUserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime CreateAt { get; set; }
        public bool Status { get; set; }
        public int? BookingId { get; set; }
        public virtual Booking? Booking { get; set; }
    }
}
