using Microsoft.EntityFrameworkCore;
using RentalCar.Model.Models;

namespace RentalCar.Data.Repositories
{
    public class CarReviewRepository : ICarReviewRepository
    {
        private readonly DataContext _context;

        public CarReviewRepository(DataContext context)
        {
            _context = context;
        }
        public List<CarReview>? GetReviewByCar(int idCar)
        {
            return _context.CarReviews.Include(r => r.User).Where(r => r.CarId == idCar).ToList();
        }
    }
}