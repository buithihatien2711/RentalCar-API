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
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            // var username = "nguyenvana";
            var user = _userService.GetUserByUsername(username);
            if (user == null) return NotFound();

            var profile = _mapper.Map<User, UserProfile>(user);
            return Ok(profile);
        }

        [HttpPut("/api/account/admin")]
        public ActionResult<UpdateAdminDto> Put(UpdateAdminDto userProfile)
        {
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            // var username = "nguyenvana";

            if(string.IsNullOrEmpty(username)) return NotFound();

            var userExist = _userService.GetUserByUsername(username);

            _mapper.Map<UpdateAdminDto, User>(userProfile, userExist);

            _userService.UpdateUser(username, userExist);

            if (_userService.SaveChanges())
            {
                var admin = _userService.GetUserByUsername(username);
                if(admin == null) return null;
                return Ok(_mapper.Map<User, AdminProfileDto>(admin));
            } 
            return BadRequest("Profile update failed");
        }

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

        [HttpPatch("/api/account")]
        public ActionResult<UserProfile> UpdateUserPatch(JsonPatchDocument userModel)
        {
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            // var username = "nguyenvana";

            if(string.IsNullOrEmpty(username)) return NotFound();

            _userService.UpdateUserPatch(username, userModel);

            if(_userService.SaveChanges()) return Ok(_mapper.Map <User, UserProfile>(_userService.GetUserByUsername(username)));
            return BadRequest("Profile update failed");
        }

        [HttpPut("/api/account/avatar")]
        public async Task<ActionResult<UserProfile>> UpdateAvatar(IFormFile userAvatar)
        {
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            // var username = "nguyenvana";

            if(string.IsNullOrEmpty(username)) return NotFound();

            var userExist = _userService.GetUserByUsername(username);

            userExist.ProfileImage = await _uploadImgService.UploadImage("avatar", username, userAvatar);

            _userService.UpdateUser(username, userExist);

            if (_userService.SaveChanges())
            {
                Dictionary<string, string> response = new Dictionary<string, string>();
                response.Add("avatar", userExist.ProfileImage);
                return Ok(response);
            } 
            return BadRequest("Avatar update failed");
        }

        [HttpPut("/api/account/license")]
        public async Task<ActionResult<LicenseViewDto>> UpdateLicense([FromForm] LicenseDto licenseDto)
        {
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            // var username = "nguyenvana";

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
    }
}