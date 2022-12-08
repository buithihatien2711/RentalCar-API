using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.API.Models
{
    public class BookingPrice
    {
        public decimal PriceAverage { get; set; }
        public decimal Total { get; set; }
        public string Schedule { get; set; }
    }
}