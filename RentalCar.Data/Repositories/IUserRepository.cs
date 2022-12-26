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

        void UpdateLicense(License license, string username);

        License? GetLicenseByUser(string username);

        List<QuantityStatistics> StatistUsersByMonth(int year);

        List<QuantityStatistics> StatistUsersByDay(int month);

        int GetNumberTripOfUser(int idUser);

        bool IsAdminAccount(int idUser);

        bool DeleteUser(User user);
        bool IsLease(string username);
    }
}