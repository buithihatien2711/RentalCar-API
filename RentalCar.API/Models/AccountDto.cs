using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.API.Models
{
    public class AccountDto
    {
        public int Id { get; set; }

        public string? ProfileImage { get; set; }
        
        public string? Fullname { get; set; }
    }
}