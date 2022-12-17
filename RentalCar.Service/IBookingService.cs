using RentalCar.Model.Models;

namespace RentalCar.Service
{
    public interface IBookingService
    {
        public void CreateBooking(Booking booking);

        bool SaveChanges();

        List<Booking> GetAllBooking ();

        Booking? GetBookingById(int idBooking);

        void ConfirmBooking(int idBooking);

        void CancelBySystem(int idBooking);

        void CancelByLease(int idBooking);

        void CancelByRenter(int idBooking);
    }
}