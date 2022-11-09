using RentalCar.Model.Models;

namespace RentalCar.Service
{
    public interface ICarService
    {
        Car GetCarById(int id);
        List<Car> GetCars();
        Car GetCarByCarname(string Carname);
        string GetImageAvtByCarId(int CarId);
        List<CarModel> GetCarModels();
        List<CarBrand> GetCarBrands();

        List<District> GetDistricts();
        List<Ward> GetWards();
    }
}