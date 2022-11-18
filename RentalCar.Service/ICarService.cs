using RentalCar.Model.Models;

namespace RentalCar.Service
{
    public interface ICarService
    {
        Car GetCarById(int id);
        List<Car> GetCars();
        Car GetCarByCarname(string Carname);
        // string GetImageAvtByCarId(int CarId);
        List<CarImage> GetImageByCarId(int CarId);

        List<CarBrand> GetCarBrands();
        //Transmission
        List<Transmission> GetTransmissions();

        // FuelType
        List<FuelType> GetFuelTypes();

        List<District> GetDistricts();

        List<Ward> GetWards();

        List<Ward> GetWardsByDictrictsId(int id);

        void CreateCar(Car car);

        Car GetCarByPateNumber(string PateNumber);

        void InsertImage(int carid,List<string> CarImage);

        //Location
        void InsertLocation(Location location);

        bool CreateLocation(Location location);

        Location GetLocationByAddress(string Address);

        //Status
        void UpdateStatusOfCar(int carId, int StatusID);

        bool SaveChanges();

        List<CarReview>? GetCarReviewsByCarId(int id);

        void DeleteCar(Car car);

        List<Car>? GetCarsByUserAndStatus(int idUser, int idStatus);

        Status? GetStatuById(int idStatus);
    }
}