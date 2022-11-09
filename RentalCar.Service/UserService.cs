using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}