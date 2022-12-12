using RentalCar.Model.Models;

namespace RentalCar.Service
{
    public interface ICarReviewService
    {
        List<CarReview>? GetReviewByCar(int idCar, int pageIndex);

        void AddCarReview(CarReview carReview);
    }
}