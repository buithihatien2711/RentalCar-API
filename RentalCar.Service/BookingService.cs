using RentalCar.Data.Repositoriess;
using RentalCar.Model.Models;
using RentalCar.Service.Models;

namespace RentalCar.Service
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICarService _carService;

        public BookingService(IBookingRepository bookingRepository, ICarService carService)
        {
            _bookingRepository = bookingRepository;
            _carService = carService;
        }

        // public void CancelByLease(int idBooking)
        // {
        //     var booking = GetBookingById(idBooking);
        //     if(booking == null) return;
        //     _bookingRepository.CancelByLease(booking);
        // }

        // public void CancelByRenter(int idBooking)
        // {
        //     var booking = GetBookingById(idBooking);
        //     if(booking == null) return;
        //     _bookingRepository.CancelByRenter(booking);
        // }

        public void CancelByUser(int idBooking, int idUser)
        {
            var booking = GetBookingById(idBooking);
            if(booking == null) return;
            _bookingRepository.CancelByUser(booking, idUser);
        }

        // public void CancelBySystemWaitConfirm(int idBooking)
        // {
        //     var booking = GetBookingById(idBooking);
        //     if(booking == null) return;
        //     _bookingRepository.CancelBySystemWaitConfirm(booking);
        // }

        // public void CancelBySystemWaitDeposit(int idBooking)
        // {
        //     var booking = GetBookingById(idBooking);
        //     if(booking == null) return;
        //     _bookingRepository.CancelBySystemWaitDeposit(booking);
        // }

        public void ConfirmBooking(int idBooking)
        {
            var booking = GetBookingById(idBooking);
            if(booking == null) return;
            _bookingRepository.ConfirmBooking(booking);
        }

        public void ConfirmReceivedCar(int idBooking)
        {
            var booking = GetBookingById(idBooking);
            if(booking == null) return;
            _bookingRepository.ConfirmReceivedCar(booking);
        }

        public void ConfirmCompleteTrip(int idBooking)
        {
            var booking = GetBookingById(idBooking);
            if(booking == null) return;
            _bookingRepository.ConfirmCompleteTrip(booking);
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

        public string GetMessageByStatus(enumStatus status)
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

                case enumStatus.CompletedTrip:
                    return "Chuyến đã hoàn thành";
                    
                // case enumStatus.CancelBySystemWaitConfirm:
                //     return "Chuyến đã bị hủy. Lý do: Hệ thống đã hủy chuyến do thời gian chờ chủ xe xác nhận quá lâu";

                // case enumStatus.CancelBySystemDeposit:
                //     return "Chuyến đã bị hủy. Lý do: Hệ thống đã hủy chuyến do quá hạn đặt cọc";
                
                case enumStatus.ReceivedCar:
                    return "Khách thuê đã nhận xe";

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

                case ((int)enumStatus.CompletedTrip):       //8
                    return "Hoàn thành chuyến đi";
                    
                // case ((int)enumStatus.CancelBySystemWaitConfirm):   //4
                //     return "Bị hủy bởi hệ thống do thời gian chờ chấp nhận quá lâu";

                // case ((int)enumStatus.CancelBySystemDeposit):   //5
                //     return "Bị hủy bởi hệ thống do khách thuê không đặt cọc";

                case ((int)enumStatus.ReceivedCar):   //5
                    return "Đã nhận xe";
                
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

        public void DepositBooking(int idBooking)
        {
            var booking = GetBookingById(idBooking);
            if(booking == null) return;
            _bookingRepository.DepositBooking(booking);
        }

        public List<Booking> GetBookingsByStatus(int idStatus)
        {
            return _bookingRepository.GetBookingsByStatus(idStatus);
        }

        public List<Booking> GetHistoryBookings(int idUser)
        {
            return _bookingRepository.GetHistoryBookings(idUser);
        }

        public List<Booking> GetCurrentBookings(int idUser)
        {
            return _bookingRepository.GetCurrentBookings(idUser);
        }

        public List<Booking> GetHistoryReservations(int idUser)
        {
            return _bookingRepository.GetHistoryReservations(idUser);
        }

        public List<Booking> GetCurrentReservations(int idUser)
        {
            return _bookingRepository.GetCurrentReservations(idUser);
        }

        public BookingPrice CalculatePriceAverage(int id, User? user, DateTime RentDate, DateTime ReturnDate)
        {
            var car = _carService.GetCarById(id);
            string message = "Thời gian đặt xe hợp lệ";
            decimal price = 0;
            int count = 0;
            for(var day = RentDate ; day <= ReturnDate ; day = day.AddDays(1)){
                count++;
                if(_carService.CheckScheduleByDate(id,day) == true) message = "Xe bận trong khoảng thời gian trên. Vui lòng đặt xe khác hoặc thay đổi lịch trình thích hợp.";
                var resultBefore = price;
                foreach(var priceDate in car.PriceByDates){
                    if(priceDate.Date.Date == day.Date) price += priceDate.Cost;
                }
                price = (price != resultBefore) ? price : resultBefore + car.Cost;
            }
            if(user !=null){
                if(car.UserId == user.Id) message = "Bạn không thể đặt xe của mình.";
            }
            return new BookingPrice{
                Day = count,
                PriceAverage = price/count,
                Total = price,
                Schedule =message
            };
        }

        public int GetRoleUserInBooking(int idBooking, int idUser)
        {
            return _bookingRepository.GetRoleUserInBooking(idBooking, idUser);
        }
        public Booking GetCurrentBooking()
        {
            return _bookingRepository.GetCurrentBooking();
        }
    }
}