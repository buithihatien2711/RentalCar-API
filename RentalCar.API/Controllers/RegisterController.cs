using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalCar.API.Models;
using RentalCar.Data;
using RentalCar.Model.Models;
using RentalCar.Service;

namespace RentalCar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly ICarService _carService;

        public RegisterController(DataContext context, ITokenService tokenService,ICarService carService)
        {
            _context = context;
            _tokenService = tokenService;
            // _carService = carService;
        }

        // [HttpPost("register")]
        [HttpPost]
        public ActionResult<string> Register(Register register)
        {
            register.UserName.ToLower();
            if(_context.Users.Any(u => u.Username == register.UserName)){
                return BadRequest("Username already exists");
            }

            using var hmac = new HMACSHA512();

            var user = new User{
                Username = register.UserName,
                Contact = register.PhoneNumber,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password)),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            var token = _tokenService.CreateToken(user);
            return Ok(token);
        }
    }
}