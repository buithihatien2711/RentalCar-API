using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentalCar.Data.Repositories;
using RentalCar.Model.Models;

namespace RentalCar_API.RentalCar.Service
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notifiRepository;

        public NotificationService(INotificationRepository notifiRepository)
        {
            _notifiRepository = notifiRepository;
        }
        public void CreateINotifi(Notification noti)
        {
            _notifiRepository.CreateINotifi(noti);
        }

        public List<Notification> NotifiByUserId(int userid)
        {
            return _notifiRepository.NotifiByUserId(userid);
        }

        public List<Notification> NotifiNotReadByUserId(int userid)
        {
            return _notifiRepository.NotifiNotReadByUserId(userid);
        }

        public bool UpdateStatusNotifi(int id)
        {
            return _notifiRepository.UpdateStatusNotifi(id);
        }
    }
}