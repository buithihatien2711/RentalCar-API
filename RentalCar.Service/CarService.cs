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

        public void CreateCar(Car car)
        {
            _carRepository.CreateCar(car);
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

        public Car GetCarByPateNumber(string PateNumber)
        {
            return _carRepository.GetCarByPateNumber(PateNumber);
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

        public List<string> GetImageByCarId(int CarId)
        {
            return _carRepository.GetImageByCarId(CarId);
        }

        // public Location GetLocationByCarId(int CarId)
        // {
        //     return _carRepository.GetLocationByCarId(CarId);
        // }

        public List<Ward> GetWards()
        {
            return _carRepository.GetWards();
        }

        public void InsertImage(int carid,List<string> CarImage)
        {
            _carRepository.InsertImage(carid,CarImage);
        }

        public bool SaveChanges()
        {
            return _carRepository.SaveChanges();
        }
    }
}