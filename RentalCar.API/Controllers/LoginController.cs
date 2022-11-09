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
    public class LoginController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly ICarService _carService;

        public LoginController(DataContext context, ITokenService tokenService,ICarService carService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        // [HttpPost("login")]
        [HttpPost]
        public ActionResult<string> Login(Login login)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == login.UserName.ToLower());
            if(user == null) return Unauthorized("Invalid Username");
            
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));
            for( var i = 0; i < computeHash.Length; i++){
                if(computeHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }
            var token = _tokenService.CreateToken(user);
            // return Ok(new UserModel{
            //     Username = user.Username,
            //     Token = token
            // });
            return Ok(token);

        }
    }
}