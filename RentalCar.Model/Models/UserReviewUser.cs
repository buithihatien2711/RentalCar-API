using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.Model.Models
{
    public class UserReviewUser
    {
        public int UserId { get; set; }
        
        public User User { get; set; }
        
        public int UserReviewId { get; set; }
        
        public UserReview UserReview { get; set; }
        
        // Role của người dùng khi viết cmt
        public int RoleId { get; set; }
    }
}