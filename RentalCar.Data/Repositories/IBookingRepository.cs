using RentalCar.Model.Models;

namespace RentalCar.Data.Repositoriess
{
    public interface IBookingRepository
    {
        public void CreateBooking(Booking booking);
        bool SaveChanges();

        List<Booking> GetAllBooking ();

        Booking? GetBookingById(int idBooking);

        void ConfirmBooking(Booking booking);

        void CancelBySystem(Booking booking);

        void CancelByLease(Booking booking);

        void CancelByRenter(Booking booking);

        void PayDeposit(Booking booking);
    }
}