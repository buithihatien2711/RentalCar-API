using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCar.Model.Models
{
    public class District
    {
        // [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        
        [MaxLength(256)]
        public string Name { get; set; }

        List<Ward> Wards { get; set; }
    }
}