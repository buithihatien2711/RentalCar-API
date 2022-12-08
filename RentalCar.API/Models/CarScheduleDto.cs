using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.API.Models
{
    public class CarScheduleDto
    {
        // public int id { get; set; }
        public DateTime rentDate { get; set; }
        public DateTime returnDate { get; set; }

    }
}