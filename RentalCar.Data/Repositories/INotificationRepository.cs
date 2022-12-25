using RentalCar.Model.Models;

namespace RentalCar.Data.Repositories
{
    public interface INotificationRepository
    {
        List<Notification> NotifiByUserId(int userid);
        bool UpdateStatusNotifi(int id);
        void CreateINotifi(Notification noti);
    }
}