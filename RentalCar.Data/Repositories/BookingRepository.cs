using RentalCar.Data.Repositoriess;
using RentalCar.Model.Models;

namespace RentalCar.Data.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly DataContext _context;

        public BookingRepository(DataContext context)
        {
            _context = context;
        }

        public void CreateBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

    }
}