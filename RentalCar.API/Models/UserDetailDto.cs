using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.API.Models
{
    public class UserDetailDto
    {
        public int Id { get; set; }

        public string Fullname { get; set; }

        public string Contact { get; set; }

        public string ProfileImage { get; set; }
        
        public string Gender { get; set; }
        
        public DateTime? DateOfBirth { get; set; }
        
        public string Username { get; set; }

        public string Email { get; set; }
        
        public byte[] PasswordSalt { get; set; }
        
        public byte[] PasswordHash { get; set; }
        
        public DateTime? CreatedAt { get; set; }
        
        public DateTime UpdateAt { get; set; }

        public List<RoleDto> Roles {get; set; }
        
        public LicenseDto LicenseDto { get; set; }
    }
}