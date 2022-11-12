using RentalCar.Model.Models;

namespace RentalCar.API.Models
{
    public class CarViewDto
    {
        public string Name { get; set; }
        public string? Plate_number { get; set; }
        public CarModel? CarModels { get; set; }
        public CarBrand? CarBrands { get; set; }
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
        public List<string>? Image { get; set; }
        //Số sao
        public decimal numberStar { get; set; }
        //Rule
        public string? Rule { get; set; }
        // public List<District>? Districts { get; set; }
        // public List<Ward>? Wards { get; set; }

    }
}