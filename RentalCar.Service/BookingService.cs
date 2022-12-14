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
                    return "Chuy???n ??ang ch??? ch??? xe x??c nh???n";

                case enumStatus.WaitDeposit:
                    return "Chuy???n ??ang ch??? ?????t c???c";

                case enumStatus.Deposited:
                    return "Chuy???n ???? ???????c ?????t c???c";

                case enumStatus.CanceledByRenter:
                    return "Chuy???n ???? b??? h???y. L?? do: b??? h???y b???i ng?????i cho thu??";

                case enumStatus.CanceledByLease:
                    return "Chuy???n ???? b??? h???y. L?? do: b??? h???y b???i ch??? xe";

                case enumStatus.CompletedTrip:
                    return "Chuy???n ???? ho??n th??nh";
                    
                // case enumStatus.CancelBySystemWaitConfirm:
                //     return "Chuy???n ???? b??? h???y. L?? do: H??? th???ng ???? h???y chuy???n do th???i gian ch??? ch??? xe x??c nh???n qu?? l??u";

                // case enumStatus.CancelBySystemDeposit:
                //     return "Chuy???n ???? b??? h???y. L?? do: H??? th???ng ???? h???y chuy???n do qu?? h???n ?????t c???c";
                
                case enumStatus.ReceivedCar:
                    return "Kh??ch thu?? ???? nh???n xe";

                default:
                    return "";
            }
            
        }

        public string GetNameStatusBookingById(int idStatus)
        {
            switch (idStatus)
            {
                case ((int)enumStatus.WaitConfirm):    //2
                    return "??ang ch??? ch??? xe ch???p nh???n";

                case ((int)enumStatus.WaitDeposit):     //1
                    return "??ang ch??? ?????t c???c";

                case ((int)enumStatus.Deposited):       //3
                    return "???? ?????t c???c";

                case ((int)enumStatus.CanceledByRenter):    //6
                    return "B??? h???y b???i kh??ch thu??";

                case ((int)enumStatus.CanceledByLease):     //7
                    return "B??? h???y b???i ch??? xe";

                case ((int)enumStatus.CompletedTrip):       //8
                    return "Ho??n th??nh chuy???n ??i";
                    
                // case ((int)enumStatus.CancelBySystemWaitConfirm):   //4
                //     return "B??? h???y b???i h??? th???ng do th???i gian ch??? ch???p nh???n qu?? l??u";

                // case ((int)enumStatus.CancelBySystemDeposit):   //5
                //     return "B??? h???y b???i h??? th???ng do kh??ch thu?? kh??ng ?????t c???c";

                case ((int)enumStatus.ReceivedCar):   //5
                    return "???? nh???n xe";
                
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

        public BookingPrice CalculatePriceAverage(int id, DateTime RentDate, DateTime ReturnDate)
        {
            var car = _carService.GetCarById(id);
            string message = "Th???i gian ?????t xe h???p l???";
            decimal price = 0;
            int count = 0;
            for(var day = RentDate ; day <= ReturnDate ; day = day.AddDays(1)){
                count++;
                if(_carService.CheckScheduleByDate(id,day) == true) message = "Xe b???n trong kho???ng th???i gian tr??n. Vui l??ng ?????t xe kh??c ho???c thay ?????i l???ch tr??nh th??ch h???p.";
                var resultBefore = price;
                foreach(var priceDate in car.PriceByDates){
                    if(priceDate.Date.Date == day.Date) price += priceDate.Cost;
                }
                price = (price != resultBefore) ? price : resultBefore + car.Cost;
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