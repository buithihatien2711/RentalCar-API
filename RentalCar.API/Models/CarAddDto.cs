using RentalCar.Model.Models;

namespace RentalCar.API.Models
{
    //Thông tin hiển thị trên trang thêm xe
    public class CarAddDto
    {
       public List<CarModelDto> CarModels { get; set; }
       public List<CarBrandDto> CarBrands { get; set; }
       public List<District> Districts { get; set; }
       public List<WardDto> Wards { get; set; }
    //    public List<int>? Capacity { get; set; }
    //    public List<int>? YearManufacture { get; set; }
    
       //Truyền động
       public List<TransmissionDto> Transmissions { get; set; }
       //Loại nhiên liệu
       public List<FuelTypeDto> FuelTypes { get; set; }

    }
}