using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentalCar.Model.Models;

namespace RentalCar.API.Models
{
    //Data lấy từ FE để thêm vào DB
    public class CarAddInfo
    {
        // public string Name { get; set; }
        public string Plate_number { get; set; }
        public int CarModelId { get; set; }
         //Số ghế
        public int Capacity{ get; set; }
        //Năm sản xuất
        public int? YearManufacture { get; set; }
        //Truyền động
        public int TransmissionId { get; set; }
        //Loại nhiên liệu
        public int FuelTypeId { get; set; }
        //Mức tiêu thụ nhiên liệu
        public int FuelConsumption { get; set; }
        public string? Description { get; set; }
        //Giá
        public decimal Cost { get; set; }
        //AddressCar
        public string Address { get; set; }
        //Ward
        public int  WardId { get; set; }
        //Số sao
        // public decimal numberStar { get; set; }
        //Rule
        public string? Rule { get; set; }
        //Image
        public List<IFormFile>? Image { get; set; }
    }
}