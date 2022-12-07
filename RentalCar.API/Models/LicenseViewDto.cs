using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.API.Models
{
    public class LicenseViewDto
    {
        public string Number { get; set; }
        
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }
        
        public string Image { get; set; }
    }
}