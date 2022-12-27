using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RentalCar.Model.Models.EnumClass;

namespace RentalCar.Model.Models
{
    public class UserReviewUser
    {
        public int UserId { get; set; }
        
        public User? User { get; set; }
        
        public int UserReviewId { get; set; }
        
        public UserReview? UserReview { get; set; }
        
        // Role của người dùng khi viết cmt 1 : lease, 2 : writer 3:renter
        public RoleUserInComment RoleId { get; set; }
    }
}