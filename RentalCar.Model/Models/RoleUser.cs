using System.ComponentModel.DataAnnotations;

namespace RentalCar.Model.Models
{
    public class RoleUser
    {
        public int UserId { get; set; }
        
        public User User { get; set; }
        
        public int RoleId { get; set; }
        
        public Role Role { get; set; }        
    }
}