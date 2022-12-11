using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IUploadImgService _uploadImgService;

        public AdminController(IUserService userService, IMapper mapper, IUploadImgService uploadImgService)
        {
            _uploadImgService = uploadImgService;
            _mapper = mapper;
            _userService = userService;
            
        }
        
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

        [Authorize(Roles="admin")]
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

        [Authorize(Roles="admin")]
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
    }
}