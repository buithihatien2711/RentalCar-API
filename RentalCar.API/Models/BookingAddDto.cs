using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.API.Models
{
    public class BookingAddDto
    {
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        // public decimal Total { get; set; }
        // public int UserId { get; set; }
        // public int CarId { get; set; }
        public string Address { get; set; }
        public int WardId { get; set; }
    }
}