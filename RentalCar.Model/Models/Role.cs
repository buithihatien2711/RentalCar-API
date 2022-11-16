using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCar.Model.Models
{
    public class Role
    {
        // [Key]
        // [System.ComponentModel.DataAnnotations.KeyAttribute()]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        
        [MaxLength(256)]
        public string Name { get; set; }

        public List<RoleUser> RoleUsers {get; set; }

        public Role(){}

        public Role(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}