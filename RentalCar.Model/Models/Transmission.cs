using System.ComponentModel.DataAnnotations;

namespace RentalCar.Model.Models
{
    public class Transmission
    {
        [Key]
        public int Id { get; set; }
        
        [MaxLength(50)]
        public string Name { get; set; }
        
        public List<Car> Cars { get; set; }        

        public Transmission(){ }

        public Transmission(string name)
        { 
            Name = name;
        }
    }
}