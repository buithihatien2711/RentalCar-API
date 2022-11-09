using RentalCar.Model.Models;

namespace RentalCar.Data.Repositories
{
    public interface ICarReposity
    {
        Car GetCarById(int id);
        List<Car> GetCars();
        
        Car GetCarByCarname(string Carname);
        string GetImageAvtByCarId(int CarId);

        List<CarModel> GetCarModels();
        List<CarBrand> GetCarBrands();
        // List<string> GetListCarModelName();
        // List<string> GetListCarBrandName();
        List<District> GetDistricts();
        List<Ward> GetWards();
    }
}