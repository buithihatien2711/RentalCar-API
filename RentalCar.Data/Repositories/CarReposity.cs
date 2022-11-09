using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentalCar.Model.Models;

namespace RentalCar.Data.Repositories
{
    public class CarReposity : ICarReposity
    {
        private readonly DataContext _context;

        public CarReposity(DataContext context)
        {
            _context = context;
        }
        public Car GetCarByCarname(string Carname)
        {
            return _context.Cars.FirstOrDefault(u => u.Name == Carname);
        }

        public Car GetCarById(int id)
        {
            return _context.Cars.FirstOrDefault(u => u.Id == id);
        }

        public List<Car> GetCars()
        {
            return _context.Cars.ToList();
        }

        public string GetImageAvtByCarId(int CarId)
        {
            return _context.CarImages.FirstOrDefault(u => u.CarId == CarId).Path;
        }
        public List<CarBrand> GetCarBrands()
        {
            return _context.CarBrands.ToList();
        }
        public List<CarModel> GetCarModels()
        {
            return _context.CarModels.ToList();
        }

        public List<District> GetDistricts()
        {
            return _context.Districts.ToList();
        }

        public List<Ward> GetWards()
        {
            return _context.Wards.ToList();
        }
    }
}