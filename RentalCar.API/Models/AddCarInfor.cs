using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.API.Models
{
    public class AddCarInfor
    {
        public string Name { get; set; }
        public string? Plate_number { get; set; }
        public int CarModelId { get; set; }
        public int CarBrandId { get; set; }
        public string? Color { get; set; }
         //Số ghế
        public int Capacity{ get; set; }
        //Năm sản xuất
        public int? YearManufacture { get; set; }
        //Truyền động
        public string? Transmission { get; set; }
        //Loại nhiên liệu
        public string? FuelType { get; set; }
        //Mức tiêu thụ nhiên liệu
        public int FuelConsumption { get; set; }
        public string? Description { get; set; }
        //Giá
        public decimal Cost { get; set; }
        //AddressCar
        public string AddressCar { get; set; }
        //Image
        //Số sao
        public decimal numberStar { get; set; }
        //Rule
        public string? Rule { get; set; }
        public List<string>? Image { get; set; }
    }
}