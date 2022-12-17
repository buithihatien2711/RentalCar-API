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

        public List<CarImage> GetImageByCarId(int CarId)
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

        public void InsertImage(int carid,string CarImage)
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

        public List<CarReview>? GetCarReviewsByCarId(int id)
        {
            return _carRepository.GetCarReviewsByCarId(id);
        }

        public void DeleteCar(Car car)
        {
            _carRepository.DeleteCar(car);
        }

        public List<Car>? GetCarsByUserAndStatus(int idUser, int idStatus)
        {
            return _carRepository.GetCarsByUserAndStatus(idUser, idStatus);
        }

        public Status? GetStatuById(int idStatus)
        {
            return _carRepository.GetStatuById(idStatus);
        }
        public void UpdateCarInfor(int carid, Location location, int fuelco, string des,decimal Cost)
        {
            _carRepository.UpdateCarInfor(carid,location,fuelco,des,Cost);
        }

        public Ward? GetWardById(int id)
        {
            return _carRepository.GetWardById(id);
        }

        public CarImage? GetCarImagebyId(int ImgId)
        {
            return _carRepository.GetCarImagebyId(ImgId);
        }

        public void DeleteCarImagebyId(int ImgId)
        {
            _carRepository.DeleteCarImagebyId(ImgId);
        }

        public List<Car>? GetCarsByStatus(int idStatus)
        {
            return _carRepository.GetCarsByStatus(idStatus);
        }

        public List<CarTypeRegister> GetCarTypeRegister()
        {
            return _carRepository.GetCarTypeRegister();
        }
        
        
        public List<CarImgRegister>? GetCarImgRegistersByCarIdAndTypeId(int CarId, int CarTypeRegisterId)
        {
            return _carRepository.GetCarImgRegistersByCarIdAndTypeId(CarId,CarTypeRegisterId);
        }

        public void InsertImageRegister(int carid, int IdType, string Images)
        {
            _carRepository.InsertImageRegister(carid,IdType,Images);
        }

        public void DeleteCarImageRgtbyId(int ImgId)
        {
            _carRepository.DeleteCarImageRgtbyId(ImgId);
        }
        
        public List<Car>? GetCarsFilterSort(SearchParam searchParam)
        {
            return _carRepository.GetCarsFilterSort(searchParam);
        }

        public bool CheckScheduleByDate(int idCar,DateTime date)
        {
            var car = GetCarById(idCar);
            foreach(var schedule in car.CarSchedules){
                for(var day= schedule.rentDate; day <= schedule.returnDate; day = day.AddDays(1)){
                    if(date.Date == day.Date) return true;
                }
            }
            return false;
        }

        public List<QuantityStatistics> StatistCarsByMonth(int year)
        {
            return _carRepository.StatistCarsByMonth(year);
        }

        public List<QuantityStatistics> StatistCarsByDay(int month)
        {
            return _carRepository.StatistCarsByDay(month);
        }

        public List<Car> GetCarsByUser(int idUser)
        {
            return _carRepository.GetCarsByUser(idUser);
        }
    }
}