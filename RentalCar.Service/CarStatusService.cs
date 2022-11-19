using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentalCar.Data.Repositories;
using RentalCar.Model.Models;

namespace RentalCar.Service
{
    public class CarStatusService : ICarStatusService
    {
        private readonly ICarStatusRepository _carStatusRepository;

        public CarStatusService(ICarStatusRepository carStatusRepository)
        {
            _carStatusRepository = carStatusRepository;
            
        }

        public List<Status> GetStatuses()
        {
            return _carStatusRepository.GetStatuses();
        }
    }
}