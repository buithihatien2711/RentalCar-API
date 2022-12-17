using System.ComponentModel.DataAnnotations;

namespace RentalCar.Model.Models
{
    public enum enumStatus
    {
        // Chủ xe đã xác nhận và đang chờ đặt cọc
        WaitDeposit = 1,

        // Chờ xác nhận
        WaitConfirm = 2,

        // Đã đặt cọc
        Deposited = 3,

        // Bị hủy bởi hệ thống
        CanceledBySystem = 4,

        // Bị hủy bởi người thuê
        CanceledByRenter = 5,
        
        // Bị hủy bởi người cho thuê
        CanceledByLease = 6,

        // Đã hoàn thành đặt xe
        Completed = 7
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