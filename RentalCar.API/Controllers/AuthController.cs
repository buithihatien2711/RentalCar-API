using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using RentalCar.API.Models;
using RentalCar.Model.Models;
using RentalCar.Service;
using RentalCar_API.RentalCar.Service;

namespace RentalCar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly INotificationService _notifiService;

        public AuthController(ITokenService tokenService,IUserService userService,INotificationService notifiService)
        {
            _tokenService = tokenService;
            _userService = userService;
            _notifiService = notifiService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody]  Register register)
        {
            try
            {
                var username = register.UserName.ToLower();
                if(_userService.GetUserByUsername(username) != null){
                    // Dictionary<string, string> message = new Dictionary<string, string>();
                    // message.Add("Message", "Username already exists");
                    // return BadRequest(message);
                    return BadRequest("Username already exists");
                }

                using var hmac = new HMACSHA512();

                var user = new User{
                    Username = register.UserName,
                    Contact = register.PhoneNumber,
                    Fullname = register.YourName,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password)),
                    PasswordSalt = hmac.Key,
                    CreatedAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                };
                _userService.CreateUser(user);
                // _userService.SaveChanges();
                if(_userService.SaveChanges()){
                    var userByUserName = _userService.GetUserByUsername(register.UserName);
                    var admin = _userService.GetUserByUsername("admin");
                    _notifiService.CreateINotifi(new Notification{
                        FromUserId = userByUserName.Id,
                        ToUserId = admin.Id,
                        CreateAt = DateTime.Now,
                        Status = false,
                        Title = "Thông báo",
                        Message = "Người dùng " + userByUserName.Username + " đã đăng kí"
                    });
                }
                var token = _tokenService.CreateToken(user);
                return Ok(new TokenDto()
                {
                    AccessToken = token
                });
            }
            catch (BadHttpRequestException ex)
            {
                // Dictionary<string, string> message = new Dictionary<string, string>();
                // message.Add("Message", ex.ToString());
                // return BadRequest(message);
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Login login)
        {
            try
            {
                // return Ok(_authService.Login(authUserDto));
                var user = _userService.GetUserByUsername(login.UserName);
                if(user == null) {
                    return Unauthorized("Username is invalid.");
                    // Dictionary<string, string> message = new Dictionary<string, string>();
                    // message.Add("Message", "Username is invalid.");
                    // return BadRequest(message);
                } 
                
                using var hmac = new HMACSHA512(user.PasswordSalt);
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));
                for( var i = 0; i < computeHash.Length; i++){
                    if(computeHash[i] != user.PasswordHash[i]){
                        return Unauthorized("Password is invalid.");
                        // Dictionary<string, string> message = new Dictionary<string, string>();
                        // message.Add("Message", "Password is invalid.");
                        // return BadRequest(message);
                    } 
                }
                var token = _tokenService.CreateToken(user);
                return Ok(new TokenDto()
                {
                    AccessToken = token
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                // Dictionary<string, string> message = new Dictionary<string, string>();
                // message.Add("Message", ex.Message);
                return BadRequest(ex);
            }
        }

        [HttpPost("/api/admin/login")]
        public IActionResult LoginAdmin([FromBody] Login login)
        {
            try
            {
                var user = _userService.GetUserByUsername(login.UserName);
                if(user == null) {
                    return Unauthorized("Username is invalid.");
                } 

                if(!_userService.IsAdminAccount(user.Id))
                {
                    return Unauthorized("Username is invalid");
                }
                
                using var hmac = new HMACSHA512(user.PasswordSalt);
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));
                for( var i = 0; i < computeHash.Length; i++){
                    if(computeHash[i] != user.PasswordHash[i]){
                        return Unauthorized("Password is invalid.");
                    } 
                }
                var token = _tokenService.CreateToken(user);
                return Ok(new TokenDto()
                {
                    AccessToken = token
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return BadRequest(ex);
            }
        }
    }
}