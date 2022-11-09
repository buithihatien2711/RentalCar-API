using RentalCar.Model.Models;

namespace RentalCar.API.Models
{
    public class CarRegister
    {
        public string Plate_number { get; set; }
        public List<CarModel> CarModels { get; set; }
        public List<CarBrand> CarBrands { get; set; }
         //Số ghế
        public int? CountChair { get; set; }
        //Năm sản xuất
        public int YearManufacture { get; set; }
        //Truyền động
        public string Transmission { get; set; }
        //Loại nhiên liệu
        public string FuelType { get; set; }
        //Mức tiêu thụ nhiên liệu
        public string FuelConsumption { get; set; }
        public string? Description { get; set; }
        //Giá
        public int Cost { get; set; }
        //AddressCar
        public string AddressCar { get; set; }
        //Image
        public string Image { get; set; }
        public List<District> Districts { get; set; }
        public List<Ward> Wards { get; set; }
    }
}