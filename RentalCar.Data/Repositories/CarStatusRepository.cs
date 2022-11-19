using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentalCar.Model.Models;

namespace RentalCar.Data.Repositories
{
    public class CarStatusRepository : ICarStatusRepository
    {
        private readonly DataContext _context;

        public CarStatusRepository(DataContext context)
        {
            _context = context;
        }

        public List<Status> GetStatuses()
        {
            return _context.Statuses.ToList();
        }
    }
}