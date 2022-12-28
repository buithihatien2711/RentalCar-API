using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.API.Models
{
    public class ReviewDto
    {
        public string Rating { get; set; }
        
        public int NumberReview { get; set; }
        
        public List<ReviewViewDto> ReviewViews { get; set; }
    }
}