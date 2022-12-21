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

        void CancelBySystemWaitConfirm(Booking booking);

        void CancelBySystemWaitDeposit(Booking booking);

        void CancelByLease(Booking booking);

        void CancelByRenter(Booking booking);

        
    }
}