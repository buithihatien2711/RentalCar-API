using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.Model.Models
{
    public class EnumClass
    {
        public enum RoleUserInComment
        {
            Lease = 1,

            Writer = 2
        }
        
        public enum Gender
        {
            MALE = 0,
            
            FEMALE = 1
        }
    }
}