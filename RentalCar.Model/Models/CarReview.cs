using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.Model.Models
{
    public class CarReview
    {
        [Key]
        public int Id { get; set; }
        
        public string Content { get; set; }
        
        public int Rating { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime UpdateAt { get; set; }
        
        public int UserId { get; set; }
        
        public User User { get; set; }
        
        public int CarId { get; set; }
        
        public Car Car { get; set; }
    }
}