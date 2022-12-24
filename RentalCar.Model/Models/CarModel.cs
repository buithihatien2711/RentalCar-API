using System.ComponentModel.DataAnnotations;

namespace RentalCar.Model.Models
{
    public class CarModel
    {
        [Key]
        public int Id { get; set; }
        
        [MaxLength(256)]
        public string? Name { get; set; }
        
        public int CarBrandId { get; set; }
        
        public CarBrand? CarBrand { get; set; }

        public List<Car>? Cars { get; set; }
    }
}