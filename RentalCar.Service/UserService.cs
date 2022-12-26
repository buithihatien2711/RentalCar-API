using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using RentalCar.Data.Repositories;
using RentalCar.Model.Models;

namespace RentalCar.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public void CreateUser(User user)
        {
            _repository.CreateUser(user);
        }

        public IEnumerable<Role> GetRolesOfUser(int idUser)
        {
            return _repository.GetRolesOfUser(idUser);
        }

        public User GetUserById(int id)
        {
            return _repository.GetUserById(id);
        }

        public User GetUserByUsername(string username)
        {
            return _repository.GetUserByUsername(username);
        }

        public IEnumerable<User> GetUsers()
        {
            return _repository.GetUsers();
        }

        public IEnumerable<User> GetUsersByRole(int idRole)
        {
            var users = _repository.GetUsersByRole(idRole);
            return users;
        }

        public bool SaveChanges()
        {
            return _repository.SaveChanges();
        }

        public void UpdateUser(string username, User user)
        {
            _repository.UpdateUser(username, user);
        }

        public Role? GetRoleById(int id)
        {
            return _repository.GetRoleById(id);
        }

        public void UpdateUserPatch(string username, JsonPatchDocument user)
        {
            _repository.UpdateUserPatch(username, user);
        }

        public void UpdateLicense(License license, string username)
        {
            _repository.UpdateLicense(license, username);
        }

        public License? GetLicenseByUser(string username)
        {
            return _repository.GetLicenseByUser(username);
        }

        public List<QuantityStatistics> StatistUsersByMonth(int year)
        {
            return _repository.StatistUsersByMonth(year);
        }

        public List<QuantityStatistics> StatistUsersByDay(int month)
        {
            return _repository.StatistUsersByDay(month);
        }

        public int GetNumberTripOfUser(int idUser)
        {
            return _repository.GetNumberTripOfUser(idUser);
        }

        public bool IsAdminAccount(int idUser)
        {
            return _repository.IsAdminAccount(idUser);
        }

        public bool DeleteUser(int id)
        {
            var user = GetUserById(id);
            return _repository.DeleteUser(user);
        }

        public bool IsLease(string username)
        {
            return _repository.IsLease(username);
        }
    }
}