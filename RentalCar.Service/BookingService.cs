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

        public void CancelBySystem(int idBooking)
        {
            var booking = GetBookingById(idBooking);
            if(booking == null) return;
            _bookingRepository.CancelBySystem(booking);
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

        public Booking? GetBookingById(int idBooking)
        {
            return _bookingRepository.GetBookingById(idBooking);
        }

        public bool SaveChanges()
        {
            return _bookingRepository.SaveChanges();
        }
    }
}