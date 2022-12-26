using System.Security.Claims;
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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        private readonly IUploadImgService _uploadImgService;

        public UsersController(IUserService userService, IMapper mapper, IUploadImgService uploadImgService)
        {
            _uploadImgService = uploadImgService;
            _mapper = mapper;
            _userService = userService;
        }
        
        // Get all user (admin)
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
                _userService.GetRolesOfUser(user.Id).ToList().ForEach(r => roles.Add(new RoleDto() { Id = r.Id, Name = r.Name }));

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
                    LicenseDto = user.License == null ? null : new LicenseViewDto()
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

        // Get all renter (admin)
        [Route("renter")]
        [HttpGet]
        public ActionResult<IEnumerable<UserProfile>> GetRenter()
        {
            //Them truong hop role co id nay ko co
            //var role = _userService.GetRoleById(2);
            //if(role == null) return NotFound();

            var userList = _userService.GetUsers();

            var users = _userService.GetUsersByRole(3);

            if (users == null) return NotFound();
            // var profiles = _mapper.Map<List<User>, List<UserProfile>>(users);
            
            var userDetails = new List<UserDto>();
            foreach (var user in users.ToList())
            {
                List<RoleDto> roles = new List<RoleDto>();
                var rolesOfUser = _userService.GetRolesOfUser(user.Id);
                if(rolesOfUser != null) rolesOfUser.ToList().ForEach(r => roles.Add(new RoleDto(){Id = r.Id, Name = r.Name}));
                
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
                    LicenseDto = user.License == null ? null : new LicenseViewDto()
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

        // Get all lease (admin)
        [Route("lease")]
        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetLease()
        {
            var role = _userService.GetRoleById(2);
            if(role == null) return NotFound();

            //Them truong hop role co id nay ko co
            var users = _userService.GetUsersByRole(2);
            if (users == null) return NotFound();
            // var profiles = _mapper.Map<List<User>, List<UserProfile>>(users);
            
            var userDetails = new List<UserDto>();
            foreach (var user in users.ToList())
            {
                List<RoleDto> roles = new List<RoleDto>();
                var rolesOfUser = _userService.GetRolesOfUser(user.Id);
                if(rolesOfUser != null) rolesOfUser.ToList().ForEach(r => roles.Add(new RoleDto(){Id = r.Id, Name = r.Name}));
                
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
                    LicenseDto = user.License == null ? null : new LicenseViewDto()
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

        // Get user by username (admin)
        [HttpGet("{username}")]
        public ActionResult<UserDetailDto> Get(string username)
        {
            var user = _userService.GetUserByUsername(username);
            if(user == null) return NotFound("user not exist");

            List<RoleDto> roles = new List<RoleDto>();
            var rolesOfUser = _userService.GetRolesOfUser(user.Id);
            if(rolesOfUser != null) rolesOfUser.ToList().ForEach(r => roles.Add(new RoleDto(){Id = r.Id, Name = r.Name}));

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
                LicenseDto = user.License == null ? null : new LicenseViewDto()
                                    {
                                        Number = user.License.Number,
                                        Name = user.License.Name,
                                        DateOfBirth = user.License.DateOfBirth,
                                        Image = user.License.Image
                                    }
            };
            return Ok(userDetail);
       }

        // Get user by id (user xem profile user khác)
        [Route("profile/{idUser}")]
        [HttpGet]
        public ActionResult<UserProfile> GetUserDetail(int idUser)
        {
            var user = _userService.GetUserById(idUser);
            if(user == null) return NotFound("user not exist");
            var profile = _mapper.Map<User, UserProfile>(user);
            profile.NumberTrip = _userService.GetNumberTripOfUser(idUser);
            return Ok(profile);
        }

        // Admin update user license
        [HttpPut("/api/admin/account/license/{username}")]
        public async Task<ActionResult<LicenseViewDto>> UpdateLicense([FromForm] LicenseDto licenseDto, string username)
        {
            if(string.IsNullOrEmpty(username)) return NotFound();

            var userLicenseExist = _userService.GetUserByUsername(username).License;

            LicenseViewDto licenseViewDto = new LicenseViewDto()
            {
                Number = licenseDto.Number,
                Name = licenseDto.Name,
                DateOfBirth = licenseDto.DateOfBirth,
                Image = await _uploadImgService.UploadImage("license", username, licenseDto.Image)
            };

            var license = _mapper.Map<LicenseViewDto, License>(licenseViewDto);

            _userService.UpdateLicense(license, username);

            if (_userService.SaveChanges())
            {
                var licenseView = _userService.GetLicenseByUser(username);
                if(licenseView == null) return null;
                return Ok(_mapper.Map<License, LicenseViewDto>(licenseView));
            } 
            return BadRequest("License update failed");
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("/api/admin/user/{id}")]
        public ActionResult<string> Delete(int id)
        {
            var user = _userService.GetUserById(id);
            if(user == null) return NotFound();

             if(_userService.DeleteUser(id))
            {
                MessageReturn success = new MessageReturn()
                {
                    StatusCode = enumMessage.Success,
                    Message = "Xóa user thành công"
                };
                return Ok(success);
            }
            MessageReturn error = new MessageReturn()
            {
                StatusCode = enumMessage.Fail,
                Message = "Xóa user thất bại"
            };
            return Ok(error);
        }
        
    }
}