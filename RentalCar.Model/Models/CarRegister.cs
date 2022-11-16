using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCar.Model.Models
{
    public class CarRegister
    {
        [Key]
        public int Id { get; set; }
        public int CarId { get; set; }
        
        public Car Car { get; set; }
        public int CarTypeRgtId { get; set; }
        
        public CarTypeRegister CarTypeRgt { get; set; }     
        public List<CarImgRegister> CarImgRegisters { get; set; } 
    }
}