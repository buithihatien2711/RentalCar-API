using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.API.Models
{
    public class BookingOverviewDto
    {
        public int BookingId { get; set; }
        
        public int CarId { get; set; }

        public string? CarImage { get; set; }

        public string CarName { get; set; }
        
        public DateTime RentDate { get; set; }
        
        public DateTime ReturnDate { get; set; }
        
        public decimal Total { get; set; }
        
        public StatusDto Status { get; set; }
        
        // Role của user: lease = 1, renter = 2
        public int RoleId { get; set; }
    }
}