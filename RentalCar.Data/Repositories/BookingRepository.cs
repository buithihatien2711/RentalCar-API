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

        /// Hệ thống hủy chuyến (nếu quá thời gian mà chủ xe chưa xác nhận)
        public void CancelBySystemWaitConfirm(Booking booking)
        {
            // Hệ thống hủy booking khi xe đang ở trạng thái chờ xác nhận
            if(booking.Status == enumStatus.WaitConfirm)
            {
                booking.Status = enumStatus.CancelBySystemWaitConfirm;
            }
        }

        /// Hệ thống hủy chuyến (nếu quá thời gian mà khách thuê chưa đặt cọc)
        public void CancelBySystemWaitDeposit(Booking booking)
        {
            // Hệ thống hủy booking khi xe đang ở trạng thái chờ xác nhận
            if(booking.Status == enumStatus.WaitDeposit)
            {
                booking.Status = enumStatus.CancelBySystemDeposit;
            }
        }

        // // Chủ xe hủy chuyến (Chưa đặt cọc)
        // public void CancelByLease(Booking booking)
        // {
        //     if(booking == null) return;
            
        //     if(booking.Status == enumStatus.WaitConfirm)
        //     {
        //         booking.Status = enumStatus.CanceledByLease;
        //     }
        // }

        // // Khách thuê xe hủy chuyến (Chưa đặt cọc)
        // public void CancelByRenter(Booking booking)
        // {
        //     if(booking == null) return;
            
        //     if(booking.Status == enumStatus.WaitConfirm)
        //     {
        //         booking.Status = enumStatus.CanceledByRenter;
        //     }
        // }

        // Chủ xe xác nhận
        
        public void ConfirmBooking(Booking booking)
        {
            // Chỉ được xác nhận khi xe đang ở trạng thái chờ xác nhận
            if(booking.Status == enumStatus.WaitConfirm)
            {
                booking.Status = enumStatus.WaitDeposit;
            }
        }
         
        public void ConfirmReceivedCar(Booking booking)
        {
            // Chỉ được xác nhận đã nhận xe khi đã đặt cọc
            if(booking.Status == enumStatus.Deposited)
            {
                booking.Status = enumStatus.ReceivedCar;
                booking.Car.StatusID = 4;
                var schedule = new CarSchedule()
                {
                    rentDate = booking.RentDate,
                    returnDate = booking.ReturnDate,
                    CarId = booking.CarId
                };
                _context.CarSchedules.Add(schedule);
            }
        }

        public void ConfirmCompleteTrip(Booking booking)
        {
            // Chỉ được xác nhận đã hoàn thành chuyến khi nhận xe
            if(booking.Status == enumStatus.ReceivedCar)
            {
                booking.Status = enumStatus.CompletedTrip;
                booking.Car.StatusID = 3;
            }
        }

        public List<Booking> GetAllBooking()
        {
            return _context.Bookings.Include(b => b.Car).ThenInclude(b => b.User)
                                    .Include(b => b.Car).ThenInclude(b => b.CarImages)
                                    .Include(b => b.User)
                                    .Include(b => b.Location)
                                    .ThenInclude(l => l.Ward)
                                    .ThenInclude(w=> w.District).ToList();
        }

        public Booking? GetBookingById(int idBooking)
        {
            return _context.Bookings.Include(b => b.Car).ThenInclude(b => b.User)
                                    .Include(b => b.Car).ThenInclude(b => b.CarImages)
                                    .Include(b => b.User)
                                    .Include(b => b.Location)
                                    .ThenInclude(l => l.Ward)
                                    .ThenInclude(w=> w.District).FirstOrDefault(b => b.Id == idBooking);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        public List<Booking> GetBookedTrip(int idUser)
        {
            return _context.Bookings.Include(b => b.Car).ThenInclude(b => b.User)
                                    .Include(b => b.Car).ThenInclude(b => b.CarImages)
                                    .Include(b => b.User)
                                    .Include(b => b.Location)
                                    .ThenInclude(l => l.Ward)
                                    .ThenInclude(w=> w.District)
                                    .Where(b => b.UserId == idUser).ToList();
        }

        public List<Booking> GetReservations(int idUser)
        {
            return _context.Bookings.Include(b => b.Car).ThenInclude(b => b.User)
                                    .Include(b => b.Car).ThenInclude(b => b.CarImages)
                                    .Include(b => b.User)
                                    .Include(b => b.Location)
                                    .ThenInclude(l => l.Ward)
                                    .ThenInclude(w=> w.District)
                                    .Where(b => b.Car.UserId == idUser).ToList();
        }

        public void CancelByUser(Booking booking, int idUser)
        {
            if(booking == null) return;

            // Renter
            if(booking.UserId == idUser)
            {
                // Người thuê được hủy trong các trường hợp: chờ đặt cọc, chờ xác nhận, đã đặt cọc
                if(booking.Status == enumStatus.WaitDeposit || 
                    booking.Status == enumStatus.WaitConfirm ||
                    booking.Status == enumStatus.Deposited)
                {
                    booking.Status = enumStatus.CanceledByRenter;
                }
            }

            if(booking.Car.UserId == idUser)
            {
                // Chủ xe được hủy trong các trường hợp: chờ đặt cọc, chờ xác nhận, đã đặt cọc(tạm thời chưa cho hủy)
                if(booking.Status == enumStatus.WaitDeposit || 
                    booking.Status == enumStatus.WaitConfirm)
                   // || booking.Status == enumStatus.Deposited)
                {
                    booking.Status = enumStatus.CanceledByLease;
                }
            }
        }

        public void DepositBooking(Booking booking)
        {
            // var existBooking = _context.Bookings.FirstOrDefault(b => b.Id == idBooking);
            if(booking == null) return;
            // Chỉ được xác nhận khi xe đang ở trạng thái chờ đặt cọc
            if(booking.Status == enumStatus.WaitDeposit)
            {
                booking.Status = enumStatus.Deposited;
            }
        }

        public List<Booking> GetBookingsByStatus(int idStatus)
        {
            return _context.Bookings.Include(b => b.Car).ThenInclude(b => b.User)
                                    .Include(b => b.Car).ThenInclude(b => b.CarImages)
                                    .Include(b => b.User)
                                    .Include(b => b.Location)
                                    .ThenInclude(l => l.Ward)
                                    .ThenInclude(w=> w.District)
                                    .Where(b => ((int)b.Status) == idStatus).ToList();
        }

        public List<Booking> GetHistoryBookings(int idUser)
        {
            return _context.Bookings.Include(b => b.Car).ThenInclude(b => b.User)
                                    .Include(b => b.Car).ThenInclude(b => b.CarImages)
                                    .Include(b => b.User)
                                    .Include(b => b.Location)
                                    .ThenInclude(l => l.Ward)
                                    .ThenInclude(w=> w.District)
                                    .Where(b => b.Status == enumStatus.CancelBySystemDeposit 
                                                || b.Status == enumStatus.CancelBySystemWaitConfirm 
                                                || b.Status == enumStatus.CanceledByLease 
                                                || b.Status == enumStatus.CanceledByRenter
                                                || b.Status == enumStatus.CompletedTrip)
                                    .Where(b => b.UserId == idUser).ToList();
        }

        public List<Booking> GetCurrentBookings(int idUser)
        {
            return _context.Bookings.Include(b => b.Car).ThenInclude(b => b.User)
                                    .Include(b => b.Car).ThenInclude(b => b.CarImages)
                                    .Include(b => b.User)
                                    .Include(b => b.Location)
                                    .ThenInclude(l => l.Ward)
                                    .ThenInclude(w=> w.District)
                                    .Where(b => b.Status == enumStatus.WaitDeposit 
                                                || b.Status == enumStatus.WaitConfirm 
                                                || b.Status == enumStatus.Deposited 
                                                || b.Status == enumStatus.ReceivedCar)
                                    .Where(b => b.UserId == idUser).ToList();
        }

        public List<Booking> GetHistoryReservations(int idUser)
        {
            return _context.Bookings.Include(b => b.Car).ThenInclude(b => b.User)
                                    .Include(b => b.Car).ThenInclude(b => b.CarImages)
                                    .Include(b => b.User)
                                    .Include(b => b.Location)
                                    .ThenInclude(l => l.Ward)
                                    .ThenInclude(w=> w.District)
                                    .Where(b => b.Status == enumStatus.CancelBySystemDeposit 
                                                || b.Status == enumStatus.CancelBySystemWaitConfirm 
                                                || b.Status == enumStatus.CanceledByLease 
                                                || b.Status == enumStatus.CanceledByRenter
                                                || b.Status == enumStatus.CompletedTrip)
                                    .Where(b => b.Car.UserId == idUser).ToList();            
        }

        public List<Booking> GetCurrentReservations(int idUser)
        {
            return _context.Bookings.Include(b => b.Car).ThenInclude(b => b.User)
                                    .Include(b => b.Car).ThenInclude(b => b.CarImages)
                                    .Include(b => b.User)
                                    .Include(b => b.Location)
                                    .ThenInclude(l => l.Ward)
                                    .ThenInclude(w=> w.District)
                                    .Where(b => b.Status == enumStatus.WaitDeposit 
                                                || b.Status == enumStatus.WaitConfirm 
                                                || b.Status == enumStatus.Deposited 
                                                || b.Status == enumStatus.ReceivedCar)
                                    .Where(b => b.Car.UserId == idUser).ToList();
        }

        public int GetRoleUserInBooking(int idBooking, int idUser)
        {
            var booking = GetBookingById(idBooking);
            if(booking == null) return -1;
            // renter
            if(booking.UserId == idUser) return 2;

            // lease
            if(booking.Car.UserId == idUser) return 1;

            return -1;
        }
    }
}