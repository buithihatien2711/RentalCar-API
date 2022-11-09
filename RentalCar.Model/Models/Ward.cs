using System.ComponentModel.DataAnnotations;

namespace RentalCar.Model.Models
{
    public class Ward
    {
        [Key]
        public int Id { get; set; }
        
        [MaxLength(256)]
        public string Name { get; set; }

        public int DistrictID  { get; set; }
        
        public District District { get; set; }

        public List<Location> Locations { get; set; }
    }
}