using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.Model.Models
{
    public class UserReview
    {
        [Key]
        public int Id { get; set; }
        
        public int Rating { get; set; }
        
        // Một review thuộc 1 người cho thuê
        public int LeaseId { get; set; }

        // Một review do 1 người thuê xe viết (ko nối quan hệ trong db)
        public int RenterId { get; set; }
        
        public string? Content { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime? UpdatedAt { get; set; }

        public List<UserReviewUser>? UserReviewUsers { get; set; }
        
    }
}