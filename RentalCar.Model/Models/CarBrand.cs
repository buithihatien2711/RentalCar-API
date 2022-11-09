using System.ComponentModel.DataAnnotations;

namespace RentalCar.Model.Models
{
    public class CarBrand
    {
        [Key]
        public int Id { get; set; }
        
        [MaxLength(256)]
        public string Name { get; set; }
        
        List<CarModel> CarModels { get; set; }
    }
}