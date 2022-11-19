using RentalCar.Data.Repositories;
using RentalCar.Model.Models;

namespace RentalCar.Service
{
    public class CarReviewService : ICarReviewService
    {
        private readonly ICarReviewRepository _carRepository;

        public CarReviewService(ICarReviewRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public List<CarReview>? GetReviewByCar(int idCar)
        {
            return _carRepository.GetReviewByCar(idCar);
        }
    }
}