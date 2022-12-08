using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.API.Models
{
    public class BookingDto
    {
        public decimal Cost { get; set; }
        public List<CarScheduleDto> Schedules { get; set; }
        public List<PriceByDateDto> PriceByDates { get; set; }
    }
}