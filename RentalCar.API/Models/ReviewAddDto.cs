using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.API.Models
{
    public class ReviewAddDto
    { 
        public int Value { get; set; }
        
        public string? Content { get; set; }
    }
}