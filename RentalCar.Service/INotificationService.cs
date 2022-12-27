using RentalCar.Model.Models;

namespace RentalCar_API.RentalCar.Service
{
    public interface INotificationService
    {
        List<Notification> NotifiByUserId(int userid);
        List<Notification> NotifiNotReadByUserId(int userid);   
        bool UpdateStatusNotifi(int id);
        void CreateINotifi(Notification noti);
    }
}