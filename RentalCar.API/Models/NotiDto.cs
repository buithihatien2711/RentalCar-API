using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.API.Models
{
    public class NotiDto
    {
        public int Id { get; set; }
        public int FromUserId{ get; set; }   
        public int ToUserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime CreateAt { get; set; }
        public bool Status { get; set; }
        public int? BookingId { get; set; }
    }
}