using RentalCar.Model.Models;

namespace RentalCar.Data.Repositories
{
    public interface ICarReviewRepository
    {
        List<CarReview>? GetReviewByCar(int idCar, int page = 1);
        
        void AddCarReview(CarReview carReview);
    }
}