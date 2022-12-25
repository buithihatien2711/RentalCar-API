using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentalCar.Model.Models;

namespace RentalCar.Data.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly DataContext _context;

        public NotificationRepository(DataContext context)
        {
            _context = context;
        }
        public void CreateINotifi(Notification noti)
        {
            _context.Notifications.Add(noti);
            _context.SaveChanges();
        }

        public List<Notification> NotifiByUserId(int userid)
        {
            return _context.Notifications.Where(p => p.ToUserId == userid).ToList();
            // return _context.Notifications.Where(p => p.ToUserId == userid).OrderBy(p => p.CreateAt).ToList();
        }

        public bool UpdateStatusNotifi(int id)
        {
            var noti = _context.Notifications.FirstOrDefault(p => p.Id == id);
            if(noti == null){
                return false;
            }
            noti.Status = true;
            _context.SaveChanges();
            return true;
        }
    }
}