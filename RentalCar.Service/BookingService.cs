using RentalCar.Data.Repositoriess;
using RentalCar.Model.Models;

namespace RentalCar.Service
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public void CancelByLease(int idBooking)
        {
            var booking = GetBookingById(idBooking);
            if(booking == null) return;
            _bookingRepository.CancelByLease(booking);
        }

        public void CancelByRenter(int idBooking)
        {
            var booking = GetBookingById(idBooking);
            if(booking == null) return;
            _bookingRepository.CancelByRenter(booking);
        }

        public void CancelBySystemWaitConfirm(int idBooking)
        {
            var booking = GetBookingById(idBooking);
            if(booking == null) return;
            _bookingRepository.CancelBySystemWaitConfirm(booking);
        }

        public void CancelBySystemWaitDeposit(int idBooking)
        {
            var booking = GetBookingById(idBooking);
            if(booking == null) return;
            _bookingRepository.CancelBySystemWaitDeposit(booking);
        }

        public void ConfirmBooking(int idBooking)
        {
            var booking = GetBookingById(idBooking);
            if(booking == null) return;
            _bookingRepository.ConfirmBooking(booking);
        }

        public void CreateBooking(Booking booking)
        {
            _bookingRepository.CreateBooking(booking);
        }

        public List<Booking> GetAllBooking()
        {
            return _bookingRepository.GetAllBooking();
        }

        public List<Booking> GetBookedTrip(int idUser)
        {
            return _bookingRepository.GetBookedTrip(idUser);
        }

        public Booking? GetBookingById(int idBooking)
        {
            return _bookingRepository.GetBookingById(idBooking);
        }

        public string GetMeasageByStatus(enumStatus status)
        {
            switch (status)
            {
                case enumStatus.WaitConfirm:
                    return "Chuyến đang chờ chủ xe xác nhận";

                case enumStatus.WaitDeposit:
                    return "Chuyến đang chờ đặt cọc";

                case enumStatus.Deposited:
                    return "Chuyến đã được đặt cọc";

                case enumStatus.CanceledByRenter:
                    return "Chuyến đã bị hủy. Lý do: bị hủy bởi người cho thuê";

                case enumStatus.CanceledByLease:
                    return "Chuyến đã bị hủy. Lý do: bị hủy bởi chủ xe";

                case enumStatus.Completed:
                    return "Chuyến đã hoàn thành";
                    
                case enumStatus.CancelBySystemWaitConfirm:
                    return "Chuyến đã bị hủy. Lý do: Hệ thống đã hủy chuyến do thời gian chờ chủ xe xác nhận quá lâu";

                case enumStatus.CancelBySystemDeposit:
                    return "Chuyến đã bị hủy. Lý do: Hệ thống đã hủy chuyến do quá hạn đặt cọc";
                
                default:
                    return "";
            }
            
        }

        public string GetNameStatusBookingById(int idStatus)
        {
            switch (idStatus)
            {
                case ((int)enumStatus.WaitConfirm):    //2
                    return "Đang chờ chủ xe chấp nhận";

                case ((int)enumStatus.WaitDeposit):     //1
                    return "Đang chờ đặt cọc";

                case ((int)enumStatus.Deposited):       //3
                    return "Đã đặt cọc";

                case ((int)enumStatus.CanceledByRenter):    //6
                    return "Bị hủy bởi khách thuê";

                case ((int)enumStatus.CanceledByLease):     //7
                    return "Bị hủy bởi chủ xe";

                case ((int)enumStatus.Completed):       //8
                    return "Hoàn thành";
                    
                case ((int)enumStatus.CancelBySystemWaitConfirm):   //4
                    return "Bị hủy bởi hệ thống do thời gian chờ chấp nhận quá lâu";

                case ((int)enumStatus.CancelBySystemDeposit):   //5
                    return "Bị hủy bởi hệ thống do khách thuê không đặt cọc";
                
                default:
                    return "";
            }
        }

        public List<Booking> GetReservations(int idUser)
        {
            return _bookingRepository.GetReservations(idUser);
        }

        public bool SaveChanges()
        {
            return _bookingRepository.SaveChanges();
        }
    }
}