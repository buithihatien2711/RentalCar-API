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
        // public IEnumerable<CarView> GetListCarView()
        // {
        //     var ListCar = GetCars();
        //     IEnumerable<CarView> ListCarView
        //     foreach (Car car in ListCar)
        //     {
        //         new CarView {
        //             ImageAvt = GetImageAvtByCarId(car.Id),
        //             Name = car.Name,
        //             Cost = car.Cost,
        //             numberStar = 5.0
        //         };
        //     }

        // }
    }
}