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
        public void CreateBooking(Booking booking)
        {
            _bookingRepository.CreateBooking(booking);
        }

        public List<Booking> GetAllBooking()
        {
            return _bookingRepository.GetAllBooking();
        }

        public bool SaveChanges()
        {
            return _bookingRepository.SaveChanges();
        }
    }
}