using RentalCar.Model.Models;

namespace RentalCar.Data.Repositories
{
    public interface INotificationRepository
    {
        List<Notification> NotifiByUserId(int userid);
        List<Notification> NotifiNotReadByUserId(int userid);   
        bool UpdateStatusNotifi(int id);
        void CreateINotifi(Notification noti);
    }
}