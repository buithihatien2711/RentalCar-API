using Microsoft.AspNetCore.JsonPatch;
using RentalCar.Data.Repositories;
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

        void UpdateLicense(License license, string username);

        License? GetLicenseByUser(string username);
        
        List<QuantityStatistics> StatistUsersByMonth(int year);

        List<QuantityStatistics> StatistUsersByDay(int month);

        int GetNumberTripOfUser(int idUser);

        bool IsAdminAccount(int idUser);
        bool DeleteUser(int id);
        bool IsLease(string username);
        MessaggeService ChangPassword(string username,string passwordBefor, string passwordAfter);
        
    }
}