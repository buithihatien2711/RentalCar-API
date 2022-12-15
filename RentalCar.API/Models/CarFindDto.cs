using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.API.Models
{
    public class CarFindDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public decimal Cost { get; set; }

        public decimal NumberStar { get; set; }

        public int NumberTrip { get; set; }

        public CarImageDtos Image { get; set; }

        public TransmissionDto Transmission { get; set; }

        public LocationDto? LocationDto { get; set; }

        public WardDto? WardDto { get; set; }
        
        public DistrictDto? DistrictDto { get; set; }
    }
}