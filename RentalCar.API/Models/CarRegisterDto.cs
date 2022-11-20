using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.API.Models
{
    public class CarRegisterDto
    {
        public int IdType { get; set; }
        public string NameType { get; set; }
        public List<CarImageDtos> listImage { get; set; }
    }
}