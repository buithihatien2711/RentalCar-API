using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentalCar.Model.Models;

namespace RentalCar.API.Models
{
    public class CarInfo_UpdateDto
    {
            public int Id { get; set; }
            public string Address { get; set; }
            public int WardId { get; set; }
            public int FuelConsumption { get; set; }
            public string Description { get; set; }
            public decimal Cost { get; set; }

    }
}