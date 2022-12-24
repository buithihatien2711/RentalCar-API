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

        void DepositBooking(Booking booking);

        // void CancelByLease(Booking booking);

        // void CancelByRenter(Booking booking);

        void CancelByUser(Booking booking, int idUser);

        // Lấy ra những booking người dùng đã đặt
        List<Booking> GetBookedTrip (int idUser);

        // Lấy ra những yêu cầu đã đặt
        List<Booking> GetReservations (int idUser);
    }
}