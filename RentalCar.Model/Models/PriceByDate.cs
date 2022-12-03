using System.ComponentModel.DataAnnotations;

namespace RentalCar.Model.Models
{
    public class PriceByDate
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Cost { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public DateTime CreatedAt { get; set; }
  
    }
}