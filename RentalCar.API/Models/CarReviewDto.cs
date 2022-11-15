using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.API.Models
{
    public class CarReviewDto
    {
        public int Id { get; set; }
        
        public string? Content { get; set; }
        
        public int Rating { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime UpdateAt { get; set; }
        
        public AccountDto? AccountDto { get; set; }
        
    }
}