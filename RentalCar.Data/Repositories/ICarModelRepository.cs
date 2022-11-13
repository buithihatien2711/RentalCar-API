using RentalCar.Model.Models;

namespace RentalCar.Data.Repositoriess
{
    public interface ICarModelRepository
    {
        List<CarModel> GetCarModels();
        List<CarModel> GetCarModelsByCarBrandId(int id);

        CarModel GetCarModelById(int id);
    }
}