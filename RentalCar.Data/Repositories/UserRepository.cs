using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using RentalCar.Model.Models;

namespace RentalCar.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public void CreateUser(User user)
        {
            // _context.RoleUsers.Add(new RoleUser());
            _context.Users.Add(user);
            _context.SaveChanges();
            var user_add = GetUserByUsername(user.Username);

            var role = new RoleUser()
            {
                UserId = user_add.Id,
                User = user_add,
                RoleId = 3,
                Role = GetRoleById(3)
            };
            _context.RoleUsers.Add(role);
        }

        public Role? GetRoleById(int id)
        {
            return _context.Roles.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Role>? GetRolesOfUser(int idUser)
        {
            var user = GetUserById(idUser);
            if (user == null) return null;

            List<RoleUser> roleUsers = _context.RoleUsers.Include(ru => ru.User).Include(ru => ru.Role)
                                        .Where(u => u.UserId == idUser).ToList();

            List<Role> roles = new List<Role>();
            foreach (var roleUser in roleUsers)
            {
                roles.Add(roleUser.Role);
            }
            return roles;
        }

        public User? GetUserById(int id)
        {
            return _context.Users.Include(u => u.License).FirstOrDefault(u => u.Id == id);
        }

        public User? GetUserByUsername(string username)
        {
            return _context.Users.Include(u => u.License).FirstOrDefault(u => u.Username == username);
        }

        public IEnumerable<User>? GetUsers()
        {
            return _context.Users.Include(u => u.License).ToList();
        }

        public IEnumerable<User>? GetUsersByRole(int idRole)
        {
            var role = _context.RoleUsers.FirstOrDefault(r => r.RoleId == idRole);

            if (role == null) return null;

            List<RoleUser> roleUsers = _context.RoleUsers.Include(ru => ru.User).Include(ru => ru.Role)
                                        .Where(r => r.RoleId == idRole).ToList();

            List <User> users = new List<User>();

            foreach (var roleUser in roleUsers)
            {
                users.Add(roleUser.User);
            }

            return users;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        public void UpdateUser(string username, User user)
        {
            var existUser = GetUserByUsername(username);
            if(user == null) return;
            existUser = user;
        }

        public void UpdateUserPatch(string username, JsonPatchDocument user)
        {
            var existUser = GetUserByUsername(username);
            if(existUser == null) return;
            user.ApplyTo(existUser);
        }

        public void UpdateLicense(License license, string username)
        {
            var existUser = GetUserByUsername(username);
            if(existUser == null) return;
            existUser.License = license;
        }

        public License? GetLicenseByUser(string username)
        {
            var existUser = GetUserByUsername(username);
            if(existUser == null) return null;
            return existUser.License;
        }

        public List<QuantityStatistics> StatistUsersByMonth(int year)
        {
            var numberUserRegister =  _context.Users
                        .Where(c => c.CreatedAt.Value.Year == year)
                        .GroupBy(c => c.CreatedAt.Value.Month)
                        .Select(group => new QuantityStatistics{
                            Time = group.Key,
                            Count = group.Count()
                        });

            return numberUserRegister.ToList();
        }

        public List<QuantityStatistics> StatistUsersByDay(int month)
        {
            var numberUserRegister =  _context.Users
                        .Where(c => c.CreatedAt.Value.Month == month && c.CreatedAt.Value.Year == DateTime.Now.Year)
                        .GroupBy(c => c.CreatedAt.Value.Day)
                        .Select(group => new QuantityStatistics{
                            Time = group.Key,
                            Count = group.Count()
                        });

            return numberUserRegister.ToList();
        }

        public int GetNumberTripOfUser(int idUser)
        {
            var cars = _context.Cars.Where(c => c.UserId == idUser);
            return _context.Cars.Where(c => c.UserId == idUser).Select(c => c.NumberTrip).Sum();
        }

        public bool IsAdminAccount(int idUser)
        {
            var check = _context.RoleUsers.Where(ru => (ru.UserId == idUser) && (ru.RoleId == 1)).Count();

            return check > 0;
        }

        public bool DeleteUser(User user)
        {
            _context.Remove(user);
            return SaveChanges();
        }

        public bool IsLease(string username)
        {
            var user = _context.Users.Include(p => p.RoleUsers)
                                .FirstOrDefault(p => p.Username == username);
            bool notLease = true;
            foreach(var role in user.RoleUsers){
                if(role.RoleId == 2){
                   notLease = false;
                }
            }
            if(notLease){
                var rolelease = new RoleUser()
                {
                    UserId = user.Id,
                    User = user,
                    RoleId = 2,
                    Role = GetRoleById(2)
                };
                _context.RoleUsers.Add(rolelease);
                return SaveChanges();
            }
            return true;
        }
    }
}