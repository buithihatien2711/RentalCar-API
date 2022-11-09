using System.ComponentModel.DataAnnotations;

namespace RentalCar.Model.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        
        [MaxLength(50)]
        public string Name { get; set; }
        
        public List<Car> Cars { get; set; }        
    }
}