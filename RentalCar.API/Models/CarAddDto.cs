using RentalCar.Model.Models;

namespace RentalCar.API.Models
{
    public class CarAddDto
    {
      public List<CapacityDto> Capacities { get; set; }
      public List<YearManufactureDto> YearManufactures { get; set; }
  
      //Truyền động
      public List<TransmissionDto> Transmissions { get; set; }
      //Loại nhiên liệu
      public List<FuelTypeDto> FuelTypes { get; set; }

    }
}