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

        // Xác nhận đã nhận xe
        void ConfirmReceivedCar(Booking booking);

        // Xác nhận đã hoàn thành chuyến
        void ConfirmCompleteTrip(Booking booking);

        void DepositBooking(Booking booking);

        // void CancelByLease(Booking booking);

        // void CancelByRenter(Booking booking);

        void CancelByUser(Booking booking, int idUser);

        // Lấy ra những booking người dùng đã đặt (tất cả status)
        List<Booking> GetBookedTrip (int idUser);

        // Lấy ra những yêu cầu đã đặt
        List<Booking> GetReservations (int idUser);
        
        List<Booking> GetBookingsByStatus(int idStatus);

        // Lấy ra những chuyến đã hoàn thành hoặc bị hủy của một user
        List<Booking> GetHistoryBookings(int idUser);

        // Lấy ra những chuyến chưa hoàn thành và cũng chưa bị hủy
        List<Booking> GetCurrentBookings(int idUser);

        // Lấy ra những booking mà người khác đã đặt của 1 user (hoàn thành hoặc bị hủy)
        List<Booking> GetHistoryReservations(int idUser);

        List<Booking> GetCurrentReservations(int idUser);
    }
}