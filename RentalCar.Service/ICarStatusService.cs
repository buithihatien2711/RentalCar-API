using RentalCar.Model.Models;

namespace RentalCar.Service
{
    public interface ICarStatusService
    {
        List<Status> GetStatuses();
    }
}