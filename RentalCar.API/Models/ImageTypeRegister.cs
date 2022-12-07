using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.API.Models
{
    public class ImageTypeRegister
    {
        public int IdType { get; set; }
        public List<IFormFile> Path { get; set; }
    }
}