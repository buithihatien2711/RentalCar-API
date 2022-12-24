using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentalCar.Model.Models;

namespace RentalCar.API.Models
{
    public class LeaseAccountDto
    {
        public int Id { get; set; }
        
        public string? Fullname { get; set; }
        
        public double Rating { get; set; }

        public string? Contact { get; set; }
    }

    public class BookingViewDto
    {
        public int BookingId { get; set; }
        
        public int CarId { get; set; }

        public string? CarImage { get; set; }
        
        public string? CarName { get; set; }
        
        public decimal NumberStar { get; set; }
        
        public DateTime RentDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public int NumberDay { get; set; }

        public LocationDto? Location { get; set; }

        public WardDto? Ward { get; set; }
        
        public DistrictDto? District { get; set; }
        
        public string? Rule { get; set; }

        public decimal Cost { get; set; }
        
        public decimal Total { get; set; }

        public decimal Deposit { get; set; }
        
        public decimal RestFee { get; set; }

        public LeaseAccountDto? LeaseAccount { get; set; }

        public StatusDto? Status { get; set; }
        
        public string? Message { get; set; }
    }
}