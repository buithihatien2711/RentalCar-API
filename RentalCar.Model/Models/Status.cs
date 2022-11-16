using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCar.Model.Models
{
    public class Status
    {
        // [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        
        [MaxLength(50)]
        public string Name { get; set; }
        
        public List<Car> Cars { get; set; } 

        public Status(){}       

        public Status(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}