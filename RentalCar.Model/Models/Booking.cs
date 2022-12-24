using System.ComponentModel.DataAnnotations;

namespace RentalCar.Model.Models
{
    public enum enumStatus
    {
        WaitDeposit = 1,    // Chủ xe đã xác nhận và đang chờ đặt cọc
        WaitConfirm = 2,    // Chờ xác nhận
        Deposited = 3,      // Đã đặt cọc
        CancelBySystemWaitConfirm = 4,      // Bị hủy bởi hệ thống do chờ xác nhận quá lâu
        CancelBySystemDeposit = 5,       // Bị hủy bởi hệ thống do khách không đặt cọc
        CanceledByRenter = 6,   // Bị hủy bởi người thuê
        CanceledByLease = 7,    // Bị hủy bởi người cho thuê
        Completed = 8,  // Đã hoàn thành đặt xe
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
        public User? User { get; set; }

        public int CarId { get; set; }
        public Car? Car { get; set; }
        public int LocationId { get; set; }
        public Location? Location { get; set; }
  
    }
}