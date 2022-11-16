using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCar.Model.Models
{
    public class CarTypeRegister
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; set; }
        
        [MaxLength(256)]
        public string Name { get; set; }

        public List<CarRegister> CarRegisters {get; set; }
        // public List<CarImgRegister> CarImgRegisters {get; set; }

        public CarTypeRegister(){}

        public CarTypeRegister(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}