using RentalCar.Model.Models;

namespace RentalCar.API.Models
{
    public class AddCarDto
    {
       public List<CarModel> CarModels { get; set; }
       public List<CarBrand> CarBrands { get; set; }
       public List<District> Districts { get; set; }
       public List<Ward> Wards { get; set; }
       public List<int>? Capacity { get; set; }
       public List<int>? YearManufacture { get; set; }
       //Truyền động
       public List<string>? Transmission { get; set; }
       //Loại nhiên liệu
       public List<string>? FuelType { get; set; }

    }
}