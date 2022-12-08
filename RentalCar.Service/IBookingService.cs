using RentalCar.Model.Models;

namespace RentalCar.Service
{
    public interface IBookingService
    {
        public void CreateBooking(Booking booking);
        bool SaveChanges();
    }
}