using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentalCar.Model.Models;

namespace RentalCar.API.Models
{
    public class AddressDto
    {
        public int IdDictrict { get; set; }
        public string Name { get; set; }
        // public List<District> Districts { get; set; }
        public List<WardDto> Wards { get; set; }
    }
}