using RentalCar.Model.Models;
using RentalCar.Service.Models;

namespace RentalCar.Service
{
    public interface IBookingService
    {
        public void CreateBooking(Booking booking);

        bool SaveChanges();

        List<Booking> GetAllBooking ();

        Booking? GetBookingById(int idBooking);

        void ConfirmBooking(int idBooking);

        // void CancelBySystemWaitConfirm(int idBooking);

        // void CancelBySystemWaitDeposit(int idBooking);

        // void CancelByLease(int idBooking);

        // void CancelByRenter(int idBooking);

        void CancelByUser(int idBooking, int idUser);

        void DepositBooking(int idBooking);

        // Xác nhận đã nhận xe
        void ConfirmReceivedCar(int idBooking);

        // Xác nhận đã hoàn thành chuyến
        void ConfirmCompleteTrip(int idBooking);

        string GetMessageByStatus(enumStatus status);

        string GetNameStatusBookingById(int idStatus);

        List<Booking> GetBookedTrip(int idUser);

        List<Booking> GetReservations (int idUser);
        
        List<Booking> GetBookingsByStatus(int idStatus);

        List<Booking> GetHistoryBookings(int idUser);

        List<Booking> GetCurrentBookings(int idUser);

        List<Booking> GetHistoryReservations(int idUser);

        List<Booking> GetCurrentReservations(int idUser);

        BookingPrice CalculatePriceAverage(int id, DateTime RentDate, DateTime ReturnDate);

        int GetRoleUserInBooking(int idBooking, int idUser);
        Booking GetCurrentBooking();
    }
}