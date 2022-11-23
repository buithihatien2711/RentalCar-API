using RentalCar.Model.Models;

namespace RentalCar.Data.Repositories
{
    public interface ICarRepository
    {
        Car? GetCarById(int id);

        List<Car> GetCars();
        
        Car GetCarByCarname(string Carname);

        Car GetCarByPateNumber(string PateNumber);

        List<District> GetDistricts();

        List<Ward> GetWards();

        List<Ward> GetWardsByDictrictsId(int id);

        void CreateCar(Car car);

        //Image
        List<CarImage> GetImageByCarId(int CarId);
        // string GetImageAvtByCarId(int CarId)

        void InsertImage(int carid, List<string> CarImage);

        bool SaveChanges();

        //Brand
        List<CarBrand> GetCarBrands();

        //Transmission
        List<Transmission> GetTransmissions();
        // Transmission GetTransmissionByCarId(int id);

        // FuelType
        List<FuelType> GetFuelTypes();

        //Location
        void InsertLocation(Location location);

        bool CreateLocation(Location location);

        Location GetLocationByAddress(string Address);

        //Status
        void UpdateStatusOfCar(int carId, int StatusID);

        List<CarReview>? GetCarReviewsByCarId(int id);

        void DeleteCar(Car car);

        List<Car>? GetCarsByUserAndStatus(int idUser, int idStatus);

        Status? GetStatuById(int idStatus);
        void UpdateCarInfor(int carid,Location location,int fuelco,string des,decimal Cost);

        Ward? GetWardById(int id);

        CarImage? GetCarImagebyId(int ImgId);
        void DeleteCarImagebyId(int ImgId);

        List<Car>? GetCarsStatus(int idStatus);

        //
        List<CarTypeRegister> GetCarTypeRegister();
        List<CarImgRegister>? GetCarImgRegistersByCarIdAndTypeId(int CarId,int CarTypeRegisterId);
        void InsertImageRegister(int carid, int IdType, List<string> Images);
        void DeleteCarImageRgtbyId(int ImgId);

    }
}