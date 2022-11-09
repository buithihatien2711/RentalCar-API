using RentalCar.Data;
using RentalCar.Data.Repositories;
using RentalCar.Model.Models;

namespace RentalCar.Service
{
    public class CarService : ICarService
    {
        private readonly ICarReposity _carRepository;

        public CarService(ICarReposity carRepository)
        {
            _carRepository = carRepository;
        }
        public Car GetCarByCarname(string Carname)
        {
            return _carRepository.GetCarByCarname(Carname);
        }
        public Car GetCarById(int id)
        {
            return _carRepository.GetCarById(id);
        }

        public List<Car> GetCars()
        {
            return _carRepository.GetCars();
        }

        public string GetImageAvtByCarId(int CarId)
        {
            return _carRepository.GetImageAvtByCarId(CarId);
        }
    }
}