using RentalCar.Model.Models;

namespace RentalCar.Data.Repositoriess
{
    public interface IBookingRepository
    {
        public void CreateBooking(Booking booking);
        bool SaveChanges();

        List<Booking> GetAllBooking ();
        
        void ConfirmBooking(Booking booking);

        Booking? GetBookingById(int idBooking);

        void CancelBySystem(Booking booking);

        void CancelByLease(Booking booking);

        void CancelByRenter(Booking booking);
    }
}