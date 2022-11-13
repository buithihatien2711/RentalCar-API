using RentalCar.Data.Repositoriess;
using RentalCar.Model.Models;

namespace RentalCar.Service
{
    public class CarModelService : ICarModelService
    {
        private readonly ICarModelRepository _carmodelRepository;

        public CarModelService(ICarModelRepository carmodelRepository)
        {
            _carmodelRepository = carmodelRepository;
        }

        public CarModel GetCarModelById(int id)
        {
            return _carmodelRepository.GetCarModelById(id);
        }

        public List<CarModel> GetCarModels()
        {
            return _carmodelRepository.GetCarModels();
        }

        public List<CarModel> GetCarModelsByCarBrandId(int id)
        {
            return _carmodelRepository.GetCarModelsByCarBrandId(id);
        }
    }
}