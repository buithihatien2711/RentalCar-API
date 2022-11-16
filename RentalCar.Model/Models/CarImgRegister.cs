using System.ComponentModel.DataAnnotations;

namespace RentalCar.Model.Models
{
    public class CarImgRegister
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(512)]
        public string Path { get; set; }

        public int CarRegisterId { get; set; }
        
        public CarRegister CarRegister { get; set; }
    }
}