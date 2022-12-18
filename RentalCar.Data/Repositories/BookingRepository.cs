using Microsoft.EntityFrameworkCore;
using RentalCar.Data.Repositoriess;
using RentalCar.Model.Models;

namespace RentalCar.Data.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly DataContext _context;

        public BookingRepository(DataContext context)
        {
            _context = context;
        }

        // booking đang chờ xác nhận
        public void CreateBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
        }

        /// Hệ thống hủy chuyến (nếu quá thời gian mà chủ xe chưa xác nhận hoặc quá giờ mà khách chưa đặt cọc)
        public void CancelBySystem(Booking booking)
        {
            if(booking == null) return;
            // Hệ thống hủy booking khi xe đang ở trạng thái chờ xác nhận hoặc chờ đặt cọc
            if(booking.Status == enumStatus.WaitConfirm || booking.Status == enumStatus.WaitDeposit)
            {
                booking.Status = enumStatus.CanceledBySystem;
            }
        }

        // Chủ xe hủy chuyến (Chưa đặt cọc)
        public void CancelByLease(Booking booking)
        {
            if(booking == null) return;
            
            if(booking.Status == enumStatus.WaitConfirm)
            {
                booking.Status = enumStatus.CanceledByLease;
            }
        }

        // Khách thuê xe hủy chuyến (Chưa đặt cọc)
        public void CancelByRenter(Booking booking)
        {
            if(booking == null) return;
            
            if(booking.Status == enumStatus.WaitConfirm)
            {
                booking.Status = enumStatus.CanceledByRenter;
            }
        }

        // Chủ xe xác nhận
        public void ConfirmBooking(Booking booking)
        {
            // var existBooking = _context.Bookings.FirstOrDefault(b => b.Id == idBooking);
            if(booking == null) return;
            // Chỉ được xác nhận khi xe đang ở trạng thái chờ xác nhận
            if(booking.Status == enumStatus.WaitConfirm)
            {
                booking.Status = enumStatus.WaitDeposit;
            }
        }
        

        public List<Booking> GetAllBooking()
        {
            return _context.Bookings
                            .Include(b => b.Car)
                            .ThenInclude(c => c.User)
                            .Include(b => b.User)
                            .Include(b => b.Location)
                            .ThenInclude(l => l.Ward)
                            .ThenInclude(w=> w.District).ToList();
        }

        public Booking? GetBookingById(int idBooking)
        {
            return _context.Bookings.FirstOrDefault(b => b.Id == idBooking);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        public void PayDeposit(Booking booking)
        {
            
        }
    }
}