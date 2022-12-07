using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.API.Models
{
    public class UpdateAdminDto
    {
        public string Fullname { get; set; }

        public string Contact { get; set; }

        public string Gender { get; set; }
        
        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }    
    }
}