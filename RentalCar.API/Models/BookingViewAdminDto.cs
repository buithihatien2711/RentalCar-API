using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentalCar.Model.Models;

namespace RentalCar.API.Models
{
    public class UserBooking
    {
        public string Username { get; set; }
        
        public string Fullname { get; set; }

        public string Contact { get; set; }
        
        
    }

    public class CarBooking
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
    }

    public class BookingViewAdminDto
    {
        public int Id { get; set; }

        public DateTime RentDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public int NumberDays { get; set; }
        
        public decimal Cost { get; set; }

        public decimal Total { get; set; }

        public StatusDto Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public UserBooking Renter { get; set; }

        public UserBooking Lease { get; set; }

        public CarBooking Car { get; set; }

        public LocationDto? Location { get; set; }

        public WardDto? Ward { get; set; }
        
        public DistrictDto? District { get; set; }
    }
}