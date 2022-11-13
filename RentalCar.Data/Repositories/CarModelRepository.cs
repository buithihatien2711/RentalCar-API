using Microsoft.EntityFrameworkCore;
using RentalCar.Data.Repositoriess;
using RentalCar.Model.Models;

namespace RentalCar.Data.Repositories
{
    public class CarModelRepository : ICarModelRepository
    {
        private readonly DataContext _context;

        public CarModelRepository(DataContext context)
        {
            _context = context;
        }
        public List<CarModel>? GetCarModels()
        {
            return _context.CarModels.Include(p => p.CarBrand).ToList();
        }

        public CarModel? GetCarModelById(int id)
        {
            return _context.CarModels.Include(p => p.CarBrand).FirstOrDefault(u => u.Id == id);
        }

        public List<CarModel> GetCarModelsByCarBrandId(int id)
        {
            return _context.CarModels.Where(p => p.CarBrandId == id).ToList();
        }
    }
}