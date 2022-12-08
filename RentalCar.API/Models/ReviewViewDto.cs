using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.API.Models
{
    public class ReviewViewDto
    {
        public int Id { get; set; }
        
        public int Rating { get; set; }
        
        public string? Content { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime? UpdatedAt { get; set; }

        public AccountDto Account { get; set; }
    }
}