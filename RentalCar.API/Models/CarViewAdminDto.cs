using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.API.Models
{
    public class CarViewAdminDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string? Color { get; set; }
        
        public int Capacity { get; set; }
        
        public string Plate_number { get; set; }
        
        public decimal Cost { get; set; }
        
        public DateTime? CreatedAt { get; set; }
        
        public DateTime? UpdateAt { get; set; }

        public CarModelDto CarModel { get; set; }

        public CarBrandDto CarBrand { get; set; }
        
        public CarStatusDto Status { get; set; }
        
        public UserOverviewDto User { get; set; }

        //Năm sản xuất
        public int? YearManufacture { get; set; }
        //Mức tiêu thụ nhiên liệu
        public int FuelConsumption { get; set; }
        //Điều khoản
        public string? Rule { get; set; }

        public decimal NumberStar { get; set; }

        public int NumberTrip { get; set; }

        public List<CarImageDtos> CarImages { get; set; }

        //Truyền động
        public TransmissionDto Transmission { get; set; }

        //Loại nhiên liệu
        public FuelTypeDto FuelType { get; set; }

        public LocationDto Location { get; set; }

        public List<CarRegisterDto> CarRegisters { get; set; }
    }
}