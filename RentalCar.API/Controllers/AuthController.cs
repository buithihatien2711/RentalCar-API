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
                    MessageReturn fail = new MessageReturn()
                    {
                        StatusCode = enumMessage.Fail,
                        Message = "Username đã tồn tại."
                    };
                    return Ok(fail);
                }

                using var hmac = new HMACSHA512();

                var user = new User{
                    Username = register.UserName,
                    Contact = register.PhoneNumber,
                    Fullname = register.YourName,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password)),
                    PasswordSalt = hmac.Key,
                    CreatedAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    ProfileImage = "https://firebasestorage.googleapis.com/v0/b/pbl6-b8cad.appspot.com/o/pbl6%2Favatar%2FAvataDefault%2FAvataDefault.jpg?alt=media&token=1c8be063-80ae-4eef-b0c0-7af84999bdb9&fbclid=IwAR3Vq7RhVsB46Knuo-hyfWyn_XkTgfCI8_u0a_DQTtQAf5D7AcSa9wor6b4"
                };
                _userService.CreateUser(user);
                // _userService.SaveChanges();
                if(_userService.SaveChanges()){
                    var userByUserName = _userService.GetUserByUsername(register.UserName);
                    // var admin = _userService.GetUserByUsername("admin");

                    var Tousers = _userService.GetUsersByRole(1);
                    List<User> admin = new List<User>();
                    foreach(var touser in Tousers){
                        // admin.Add(touser);
                        _notifiService.CreateINotifi(new Notification{
                            FromUserId = userByUserName.Id,
                            ToUserId = touser.Id,
                            CreateAt = DateTime.Now,
                            Status = false,
                            Title = "Thông báo",
                            Message = "Người dùng " + userByUserName.Username + " đã đăng kí"
                        });
                    }
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
                    MessageReturn fail = new MessageReturn()
                    {
                        StatusCode = enumMessage.Fail,
                        Message = "Username không đúng."
                    };
                    return Ok(fail);
                    // return Unauthorized("Username is invalid.");
                } 
                
                using var hmac = new HMACSHA512(user.PasswordSalt);
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));
                for( var i = 0; i < computeHash.Length; i++){
                    if(computeHash[i] != user.PasswordHash[i]){
                        MessageReturn fail = new MessageReturn()
                        {
                            StatusCode = enumMessage.Fail,
                            Message = "Password không đúng."
                        };
                        return Ok(fail);
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
                    MessageReturn fail = new MessageReturn()
                    {
                        StatusCode = enumMessage.Fail,
                        Message = "Username không hợp lệ."
                    };
                    return Ok(fail);
                } 

                if(!_userService.IsAdminAccount(user.Id))
                {
                    // return Unauthorized("Username is invalid");
                    MessageReturn fail = new MessageReturn()
                    {
                        StatusCode = enumMessage.Fail,
                        Message = "Username không hợp lệ."
                    };
                    return Ok(fail);
                }
                
                using var hmac = new HMACSHA512(user.PasswordSalt);
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));
                for( var i = 0; i < computeHash.Length; i++){
                    if(computeHash[i] != user.PasswordHash[i]){
                        // return Unauthorized("Password is invalid.");
                        MessageReturn fail = new MessageReturn()
                        {
                            StatusCode = enumMessage.Fail,
                            Message = "Password không hợp lệ."
                        };
                        return Ok(fail);
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