using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RentalCar.API.Models;
using RentalCar.Model.Models;
using RentalCar.Service;

namespace RentalCar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ICarService _carService;
        private readonly IUploadImgService _uploadImgService;

        public AccountController(IUserService userService, IMapper mapper, ICarService carService, IUploadImgService uploadImgService)
        {
            _uploadImgService = uploadImgService;
            _carService = carService;
            _userService = userService;
            _mapper = mapper;
        }
        
        [Route("/api/profile/me")]
        [HttpGet]
        public ActionResult<UserProfile> Get()
        {
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier);
            // var username = "nguyenvana";
            if (username == null) return Unauthorized();

            var user = _userService.GetUserByUsername(username.Value);
            var profile = _mapper.Map<User, UserProfile>(user);
            return Ok(profile);
        }

        [HttpPatch("/api/account")]
        public ActionResult<UserProfile> UpdateUserPatch(JsonPatchDocument userModel)
        {
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier);
            // var username = "nguyenvana";

            if(username == null) return Unauthorized();

            _userService.UpdateUserPatch(username.Value, userModel);

            if(_userService.SaveChanges())
            {
                MessageReturn success = new MessageReturn()
                {
                    StatusCode = enumMessage.Success,
                    Message = "Cập nhật thông tin thành công"
                };
                return Ok(success);
                // return Ok(_mapper.Map <User, UserProfile>(_userService.GetUserByUsername(username.Value)));
            }

            MessageReturn error = new MessageReturn()
            {
                StatusCode = enumMessage.Fail,
                Message = "Cập nhật thông tin thất bại"
            };
            return Ok(error);
            // return BadRequest("Profile update failed");
        }

        [HttpPut("/api/account/avatar")]
        public async Task<ActionResult<UserProfile>> UpdateAvatar(IFormFile userAvatar)
        {
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier);
            // var username = "nguyenvana";

            if(username == null) return Unauthorized();

            var userExist = _userService.GetUserByUsername(username.Value);

            userExist.ProfileImage = await _uploadImgService.UploadImage("avatar", username.Value, userAvatar);

            _userService.UpdateUser(username.Value, userExist);

            if (_userService.SaveChanges())
            {
                // Dictionary<string, string> response = new Dictionary<string, string>();
                // response.Add("profileImage", userExist.ProfileImage);
                // return Ok(response);

                MessageReturn success = new MessageReturn()
                {
                    StatusCode = enumMessage.Success,
                    Message = "Cập nhật ảnh đại diện thành công"
                };
                return Ok(success);
            } 

            MessageReturn error = new MessageReturn()
            {
                StatusCode = enumMessage.Fail,
                Message = "Cập nhật ảnh đại diện thất bại"
            };
            return Ok(error);

            // return BadRequest("Avatar update failed");
        }

        [HttpPut("/api/account/license")]
        public async Task<ActionResult<LicenseViewDto>> UpdateLicense([FromForm] LicenseDto licenseDto)
        {
            var usernameClaim = this.User.FindFirst(ClaimTypes.NameIdentifier);
            // var username = "nguyenvana";

            if(usernameClaim == null) return Unauthorized();
            string username = usernameClaim.Value;
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
                MessageReturn success = new MessageReturn()
                {
                    StatusCode = enumMessage.Success,
                    Message = "Cập nhật giấy phép lái xe thành công"
                };
                return Ok(success);
            } 

            MessageReturn error = new MessageReturn()
            {
                StatusCode = enumMessage.Fail,
                Message = "Cập nhật giấy phép lái xe thất bại"
            };
            return Ok(error);
            // return BadRequest("License update failed");
        }

        [HttpGet("/api/account/license")]
        public async Task<ActionResult<LicenseViewDto>> GetLicense()
        {
            var usernameClaim = this.User.FindFirst(ClaimTypes.NameIdentifier);

            if(usernameClaim == null) return Unauthorized();

            string username = usernameClaim.Value;

            // var userLicenseExist = _userService.GetUserByUsername(username).License;

            var license = _userService.GetLicenseByUser(username);
            if(license == null) return null;
            return Ok(_mapper.Map<License, LicenseViewDto>(license));
        }
    
        // Update profile admin
        [Authorize]
        [HttpPut("/api/admin/account")]
        public ActionResult<UpdateUserDto> Put(UpdateUserDto userProfile)
        {
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            // var username = "nguyenvana";

            if(string.IsNullOrEmpty(username)) return NotFound();

            var userExist = _userService.GetUserByUsername(username);

            _mapper.Map<UpdateUserDto, User>(userProfile, userExist);

            _userService.UpdateUser(username, userExist);

            if (_userService.SaveChanges())
            {
                var admin = _userService.GetUserByUsername(username);
                if(admin == null) return null;
                return Ok(_mapper.Map<User, AdminProfileDto>(admin));
            } 
            return BadRequest("Profile update failed");
        }

        // Profile admin
        [Route("/api/admin/profile/me")]
        [HttpGet]
        public ActionResult<AdminProfileDto> GetProfileAdmin()
        {
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            // var username = "nguyenvana";
            var user = _userService.GetUserByUsername(username);
            if (user == null) return NotFound();

            var profile = _mapper.Map<User, AdminProfileDto>(user);
            return Ok(profile);
        }

        // Admin update user profile
        [HttpPut("/api/admin/account/{username}")]
        public ActionResult<UpdateUserDto> UpdateUserProfile(UpdateUserDto userProfile, string username)
        {
            if(string.IsNullOrEmpty(username)) return NotFound();

            var userExist = _userService.GetUserByUsername(username);

            _mapper.Map<UpdateUserDto, User>(userProfile, userExist);

            _userService.UpdateUser(username, userExist);

            if (_userService.SaveChanges())
            {
                var admin = _userService.GetUserByUsername(username);
                if(admin == null) return null;
                return Ok(_mapper.Map<User, UpdateUserDto>(admin));
            } 
            return BadRequest("Profile update failed");
        }

        [HttpPut("/api/account/isLease")]
        public ActionResult<string> Put()
        {
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if(_userService.IsLease(username)){
                MessageReturn succcess = new MessageReturn()
                    {
                        StatusCode = enumMessage.Fail,
                        Message = "Bạn đã trở thành chủ xe."
                    };
                    return Ok(succcess);
            }
            MessageReturn fail = new MessageReturn()
                    {
                        StatusCode = enumMessage.Fail,
                        Message = "Trở thành chủ xe thất bại."
                    };
                    return Ok(fail);
        }

        [HttpPut("/api/account/changePassword")]
        public ActionResult<string> Put(string Password)
        {
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if(_userService.ChangPassword(username,Password)){
                MessageReturn success = new MessageReturn()
                {
                    StatusCode = enumMessage.Success,
                    Message = "Thay đổi mật khẩu thành công"
                };
                return Ok(success);
            }
            MessageReturn fail = new MessageReturn()
            {
                StatusCode = enumMessage.Fail,
                Message = "Thay đổi mật khẩu thất bại"
            };
            return Ok(fail);
        }
        
    }
}