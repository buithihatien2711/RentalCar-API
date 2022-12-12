using Microsoft.EntityFrameworkCore;
using RentalCar.Model.Models;

namespace RentalCar.Data.Repositories
{
    public class CarReviewRepository : ICarReviewRepository
    {
        public static int PAGE_SIZE { get; set; } = 4;

        private readonly DataContext _context;

        public CarReviewRepository(DataContext context)
        {
            _context = context;
        }
        public List<CarReview>? GetReviewByCar(int idCar, int page = 1)
        {
            return _context.CarReviews.Include(r => r.User).Where(r => r.CarId == idCar)
                    .Skip((page - 1)*PAGE_SIZE).Take(PAGE_SIZE).ToList();
        }

        public void AddCarReview(CarReview carReview)
        {
            _context.Add(carReview);
            _context.SaveChanges();

            var car = _context.Cars.FirstOrDefault(r => r.Id == carReview.CarId);
            if(car == null) return;
            var carRatings = _context.CarReviews.Where(r => r.CarId == car.Id).Select(r => r.Rating);
            car.NumberStar = Convert.ToDecimal(carRatings.Average());
            _context.SaveChanges();
        }
    }
}