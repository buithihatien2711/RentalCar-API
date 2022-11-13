using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentalCar.Model.Models;

namespace RentalCar.Service
{
    public interface ICarModelService
    {
        List<CarModel> GetCarModels();
        List<CarModel> GetCarModelsByCarBrandId(int id);
        CarModel GetCarModelById(int id);
    }
}