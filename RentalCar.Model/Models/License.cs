using System.ComponentModel.DataAnnotations;

namespace RentalCar.Model.Models
{
    public class License
    {
        [Key]
        public int Id { get; set; }
        
        [MaxLength(256)]
        public string? Number { get; set; }
        
        public string? Name { get; set; }

        public DateTime DateOfBirth { get; set; }
        
        public string? Image { get; set; }
        
        public int? UserId { get; set; }
        
        public User? User { get; set; }
    }
}