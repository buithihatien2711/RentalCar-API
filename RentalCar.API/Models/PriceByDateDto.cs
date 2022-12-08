using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.API.Models
{
    public class PriceByDateDto
    {
        public DateTime Date { get; set; }
        public decimal Cost { get; set; }
    }
}