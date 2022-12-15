using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.API.Models
{
    public class MyCarOverview
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public decimal Cost { get; set; }
        
        public LocationDto? LocationDto { get; set; }

        public WardDto? WardDto { get; set; }
        
        public DistrictDto? DistrictDto { get; set; }

        public string Status { get; set; }

        public string? Image { get; set; }
        
    }
}