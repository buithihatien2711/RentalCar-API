using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.Model.Models
{
    public class CarSchedule
    {
        [Key]
        public int Id { get; set; }
        
        public DateTime? rentDate { get; set; }
        
        public DateTime? returnDate { get; set; }
        
        public int CarId { get; set; }
        
        public Car Car { get; set; }
    }
}