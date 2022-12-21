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

        void CancelBySystemWaitConfirm(int idBooking);

        void CancelBySystemWaitDeposit(int idBooking);

        void CancelByLease(int idBooking);

        void CancelByRenter(int idBooking);

        string GetMeasageByStatus(enumStatus status);

        string GetNameStatusBookingById(int idStatus);
    }
}