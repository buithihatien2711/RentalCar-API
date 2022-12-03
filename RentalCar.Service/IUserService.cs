using Microsoft.AspNetCore.JsonPatch;
using RentalCar.Model.Models;

namespace RentalCar.Service
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();

        User GetUserById(int id);

        User GetUserByUsername(string username);

        IEnumerable<User> GetUsersByRole(int idRole);

        IEnumerable<Role> GetRolesOfUser(int idUser);

        void UpdateUser(string username, User user);

        void CreateUser(User user);

        bool SaveChanges();

        Role? GetRoleById(int id);

        void UpdateUserPatch(string username, JsonPatchDocument user);
    }
}