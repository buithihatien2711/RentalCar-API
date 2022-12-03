using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using RentalCar.Model.Models;

namespace RentalCar.Data.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User>? GetUsers();

        User? GetUserById(int id);

        User? GetUserByUsername(string username);

        IEnumerable<User>? GetUsersByRole(int idRole);

        IEnumerable<Role>? GetRolesOfUser(int idUser);

        void UpdateUser(string username, User user);
        
        void UpdateUserPatch(string username, JsonPatchDocument user);

        void CreateUser(User user);

        bool SaveChanges();

        Role? GetRoleById(int id);
    }
}