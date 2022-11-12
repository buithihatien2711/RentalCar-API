using RentalCar.Model.Models;

namespace RentalCar.Data.Repositories
{
    public interface ICarReposity
    {
        Car GetCarById(int id);
        List<Car> GetCars();
        
        Car GetCarByCarname(string Carname);
        Car GetCarByPateNumber(string PateNumber);
        string GetImageAvtByCarId(int CarId);
        List<string> GetImageByCarId(int CarId);

        List<CarModel> GetCarModels();
        List<CarBrand> GetCarBrands();
        List<District> GetDistricts();
        List<Ward> GetWards();
        void CreateCar(Car car);
        void InsertImage(int carid, List<string> CarImage);
        bool SaveChanges();
    }
}