using System.ComponentModel.DataAnnotations;

namespace RentalCar.Model.Models
{
    public class FuelType
    {
        [Key]
        public int Id { get; set; }
        
        [MaxLength(50)]
        public string Name { get; set; }
        
        public List<Car> Cars { get; set; }        
    }
}