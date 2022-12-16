using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.API.Models
{
    public class CarDetailAdminDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Plate_number { get; set; }

        public string? Description { get; set; }
        
        public int Capacity { get; set; }
        
        public decimal Cost { get; set; }
        
        public CarModelDto CarModel { get; set; }
        
        public CarBrandDto CarBrand { get; set; }

        //Truyền động
        public TransmissionDto? TransmissionDto { get; set; }

        //Loại nhiên liệu
        public FuelTypeDto? FuelTypeDto { get; set; }

        //Mức tiêu thụ nhiên liệu
        public int? FuelConsumption { get; set; }

        //AddressCar
        public LocationDto? LocationDto { get; set; }

        public WardDto? WardDto { get; set; }
        
        public DistrictDto? DistrictDto { get; set; }
        
        //Điều khoản
        public string? Rule { get; set; }

        public decimal NumberStar { get; set; }

        public List<CarImageDtos>? CarImageDtos { get; set; }

        public AccountDto Account { get; set; }

    }
}