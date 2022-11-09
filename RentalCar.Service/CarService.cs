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

        public List<CarBrand> GetCarBrands()
        {
            return _carRepository.GetCarBrands();
        }

        public Car GetCarByCarname(string Carname)
        {
            return _carRepository.GetCarByCarname(Carname);
        }
        public Car GetCarById(int id)
        {
            return _carRepository.GetCarById(id);
        }

        public List<CarModel> GetCarModels()
        {
            return _carRepository.GetCarModels();
        }

        public List<Car> GetCars()
        {
            return _carRepository.GetCars();
        }

        public List<District> GetDistricts()
        {
            return _carRepository.GetDistricts();

        }

        public string GetImageAvtByCarId(int CarId)
        {
            return _carRepository.GetImageAvtByCarId(CarId);
        }

        // public Location GetLocationByCarId(int CarId)
        // {
        //     return _carRepository.GetLocationByCarId(CarId);
        // }

        public List<Ward> GetWards()
        {
            return _carRepository.GetWards();
        }
    }
}