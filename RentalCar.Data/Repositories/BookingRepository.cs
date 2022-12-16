using Microsoft.EntityFrameworkCore;
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

        // booking đang chờ xác nhận
        public void CreateBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
        }

        // Chủ xe xác nhận
        

        public List<Booking> GetAllBooking()
        {
            return _context.Bookings
                            .Include(b => b.Car)
                            .Include(b => b.User)
                            .Include(b => b.Location)
                            .ThenInclude(l => l.Ward)
                            .ThenInclude(w=> w.District).ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

    }
}