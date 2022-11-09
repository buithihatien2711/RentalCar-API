using System.ComponentModel.DataAnnotations;

namespace RentalCar.Model.Models
{
    public class CarImage
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(512)]
        public string Path { get; set; }

        public int CarId { get; set; }
        
        public Car Car { get; set; }
    }
}