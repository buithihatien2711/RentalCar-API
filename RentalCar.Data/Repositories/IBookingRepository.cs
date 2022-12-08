using RentalCar.Model.Models;

namespace RentalCar.Data.Repositoriess
{
    public interface IBookingRepository
    {
        public void CreateBooking(Booking booking);
        bool SaveChanges();
    }
}