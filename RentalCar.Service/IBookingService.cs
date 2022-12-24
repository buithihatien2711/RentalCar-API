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

        // void CancelByLease(int idBooking);

        // void CancelByRenter(int idBooking);

        void CancelByUser(int idBooking, int idUser);

        void DepositBooking(int idBooking);

        string GetMeasageByStatus(enumStatus status);

        string GetNameStatusBookingById(int idStatus);

        List<Booking> GetBookedTrip(int idUser);

        List<Booking> GetReservations (int idUser);
        
        List<Booking> GetBookingsByStatus(int idStatus);
    }
}