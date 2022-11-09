using RentalCar.Model.Models;

namespace RentalCar.Data.Repositories
{
    public interface ICarReposity
    {
        Car GetCarById(int id);
        List<Car> GetCars();
        
        Car GetCarByCarname(string Carname);
        string GetImageAvtByCarId(int CarId);
    }
}