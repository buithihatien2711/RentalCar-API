using RentalCar.Data;
using RentalCar.Data.Repositories;
using RentalCar.Model.Models;

namespace RentalCar.Service
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public void CreateCar(Car car)
        {
            _carRepository.CreateCar(car);
        }


        public bool CreateLocation(Location location)
        {
            return _carRepository.CreateLocation(location);
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

        public List<Car> GetCars()
        {
            return _carRepository.GetCars();
        }

        public List<District> GetDistricts()
        {
            return _carRepository.GetDistricts();

        }


        public List<FuelType> GetFuelTypes()
        {
            return _carRepository.GetFuelTypes();
        }

        // public string GetImageAvtByCarId(int CarId)
        // {
        //     return _carRepository.GetImageAvtByCarId(CarId);
        // }

        public List<string> GetImageByCarId(int CarId)
        {
            return _carRepository.GetImageByCarId(CarId);
        }

        public Location GetLocationByAddress(string Address)
        {
            return _carRepository.GetLocationByAddress(Address);
        }

        public List<Transmission> GetTransmissions()
        {
            return _carRepository.GetTransmissions();
        }

        // public Location GetLocationByCarId(int CarId)
        // {
        //     return _carRepository.GetLocationByCarId(CarId);
        // }

        public List<Ward> GetWards()
        {
            return _carRepository.GetWards();
        }

        public List<Ward> GetWardsByDictrictsId(int id)
        {
            return _carRepository.GetWardsByDictrictsId(id);
        }

        public void InsertImage(int carid,List<string> CarImage)
        {
            _carRepository.InsertImage(carid,CarImage);
        }

        public void InsertLocation(Location location)
        {
            _carRepository.InsertLocation(location);
        }

        public bool SaveChanges()
        {
            return _carRepository.SaveChanges();
        }

        public void UpdateStatusOfCar(int carId, int StatusID)
        {
            _carRepository.UpdateStatusOfCar(carId,StatusID);
        }
    }
}