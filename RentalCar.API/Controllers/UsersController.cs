using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalCar.API.Models;
using RentalCar.Model.Models;
using RentalCar.Service;

namespace RentalCar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(Roles = "admin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _mapper = mapper;
            _userService = userService;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<UserProfile>> Get()
        {
            var users = _userService.GetUsers().ToList();
            if (users == null) return NotFound();
            // var profiles = _mapper.Map<List<User>, List<UserProfile>>(users);

                        var userDetails = new List<UserDto>();
            foreach (var user in users)
            {
                List<RoleDto> roles = new List<RoleDto>();
                _userService.GetRolesOfUser(user.Id).ToList().ForEach(r => roles.Add(new RoleDto(){Id = r.Id, Name = r.Name}));
                
                userDetails.Add(new UserDto()
                {
                    Id = user.Id,
                    Fullname = user.Fullname,
                    Contact = user.Contact,
                    ProfileImage = user.ProfileImage,
                    Gender = user.Gender,
                    DateOfBirth = user.DateOfBirth,
                    Username = user.Username,
                    Email = user.Email,
                    PasswordSalt = user.PasswordSalt,
                    PasswordHash = user.PasswordHash,
                    CreatedAt = user.CreatedAt,
                    Roles = roles,
                    LicenseDto = user.License == null ? null : new LicenseDto()
                                        {
                                            Number = user.License.Number,
                                            Name = user.License.Name,
                                            DateOfBirth = user.License.DateOfBirth,
                                            Image = user.License.Image
                                        }
                });
            }
            
            return Ok(userDetails);
        }


        [Route("Role/{role}")]
        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> Get(int role)
        {
            var users = _userService.GetUsersByRole(role).ToList();
            if (users == null) return NotFound();
            // var profiles = _mapper.Map<List<User>, List<UserProfile>>(users);
            
            var userDetails = new List<UserDto>();
            foreach (var user in users)
            {
                List<RoleDto> roles = new List<RoleDto>();
                _userService.GetRolesOfUser(user.Id).ToList().ForEach(r => roles.Add(new RoleDto(){Id = r.Id, Name = r.Name}));
                
                userDetails.Add(new UserDto()
                {
                    Id = user.Id,
                    Fullname = user.Fullname,
                    Contact = user.Contact,
                    ProfileImage = user.ProfileImage,
                    Gender = user.Gender,
                    DateOfBirth = user.DateOfBirth,
                    Username = user.Username,
                    Email = user.Email,
                    PasswordSalt = user.PasswordSalt,
                    PasswordHash = user.PasswordHash,
                    CreatedAt = user.CreatedAt,
                    Roles = roles,
                    LicenseDto = user.License == null ? null : new LicenseDto()
                                        {
                                            Number = user.License.Number,
                                            Name = user.License.Name,
                                            DateOfBirth = user.License.DateOfBirth,
                                            Image = user.License.Image
                                        }
                });
            }
            
            return Ok(userDetails);
        }

        [HttpGet("{username}")]
        public ActionResult<UserDetailDto> Get(string username)
        {
            var user = _userService.GetUserByUsername(username);
            if(user == null) return NotFound("user not exist");
            // var userProfile = _mapper.Map<User, UserProfile>(user);
            // Role[] roles = _userService.
            List<RoleDto> roles = new List<RoleDto>();
            _userService.GetRolesOfUser(user.Id).ToList().ForEach(r => roles.Add(new RoleDto(){Id = r.Id, Name = r.Name}));

            var userDetail = new UserDetailDto()
            {
                Id = user.Id,
                Fullname = user.Fullname,
                Contact = user.Contact,
                ProfileImage = user.ProfileImage,
                Gender = user.Gender,
                DateOfBirth = user.DateOfBirth,
                Username = user.Username,
                Email = user.Email,
                PasswordSalt = user.PasswordSalt,
                PasswordHash = user.PasswordHash,
                CreatedAt = user.CreatedAt,
                Roles = roles,
                LicenseDto = user.License == null ? null : new LicenseDto()
                                    {
                                        Number = user.License.Number,
                                        Name = user.License.Name,
                                        DateOfBirth = user.License.DateOfBirth,
                                        Image = user.License.Image
                                    }
            };
            return Ok(userDetail);
       }

        // [HttpPost]
        // public void Post([FromBody] string value)
        // {
        // }

        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody] string value)
        // {
        // }

        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}