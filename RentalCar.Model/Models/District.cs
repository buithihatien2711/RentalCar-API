using System.ComponentModel.DataAnnotations;

namespace RentalCar.Model.Models
{
    public class District
    {
        [Key]
        public int Id { get; set; }
        
        [MaxLength(256)]
        public string Name { get; set; }

        List<Ward> Wards { get; set; }
    }
}