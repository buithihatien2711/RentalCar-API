using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.API.Models
{
    public class CarImgRegisterDto
    {
        public List<CarImageDtos> CaVet { get; set; }
        public List<CarImageDtos> DangKiem { get; set; }
        public List<CarImageDtos> BaoHiem { get; set; }
    }
}