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

            if(_userService.SaveChanges()) return Ok(_mapper.Map <User, UserProfile>(_userService.GetUserByUsername(username.Value)));
            return BadRequest("Profile update failed");
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
                Dictionary<string, string> response = new Dictionary<string, string>();
                response.Add("avatar", userExist.ProfileImage);
                return Ok(response);
            } 
            return BadRequest("Avatar update failed");
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
                return NoContent();
            } 
            return BadRequest("License update failed");
        }

        [HttpGet("/api/account/license")]
        public async Task<ActionResult<LicenseViewDto>> GetLicense()
        {
            var usernameClaim = this.User.FindFirst(ClaimTypes.NameIdentifier);

            if(usernameClaim == null) return Unauthorized();

            string username = usernameClaim.Value;

            var userLicenseExist = _userService.GetUserByUsername(username).License;

            var license = _userService.GetLicenseByUser(username);
            if(license == null) return null;
            return Ok(_mapper.Map<License, LicenseViewDto>(license));
        }
    }
}