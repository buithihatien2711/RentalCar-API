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

        public void AddCarReview(CarReview carReview)
        {
            _carRepository.AddCarReview(carReview);
        }

        public List<CarReview>? GetReviewByCar(int idCar, int pageIndex)
        {
            return _carRepository.GetReviewByCar(idCar, pageIndex);
        }
    }
}