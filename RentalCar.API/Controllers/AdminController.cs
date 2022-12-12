using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalCar.API.Models;
using RentalCar.Data.Repositories;
using RentalCar.Model.Models;
using RentalCar.Service;

namespace RentalCar.API.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IUploadImgService _uploadImgService;
        private readonly ICarService _carService;

        public AdminController(IUserService userService, IMapper mapper, IUploadImgService uploadImgService, ICarService carService)
        {
            _uploadImgService = uploadImgService;
            _carService = carService;
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

        [Route("/api/admin/statist/month/{year}")]
        [HttpGet]
        public ActionResult<Dictionary<string, int[]>> StatistCarsByMonth(int year)
        {
            var numberCarRegister = _carService.StatistCarsByMonth(year);
            var numberUserRegister = _userService.StatistUsersByMonth(year);
            int[] numberCarRegisterArr = new int[12];
            int[] numberUserRegisterArr = new int[12];

            for (var i = 0; i < numberCarRegisterArr.Length; i++)
            {
                numberCarRegisterArr[i] = 0;
            }
            for (var i = 0; i < numberCarRegister.Count ; i++)
            {
                numberCarRegisterArr[numberCarRegister[i].Time - 1] = numberCarRegister[i].Count;
            }

            for (var i = 0; i < numberUserRegisterArr.Length; i++)
            {
                numberUserRegisterArr[i] = 0;
            }
            for (var i = 0; i < numberUserRegister.Count ; i++)
            {
                numberUserRegisterArr[numberUserRegister[i].Time - 1] = numberUserRegister[i].Count;
            }

            Dictionary<string, int[]> statist = new Dictionary<string, int[]>();
            statist.Add("numberCars", numberCarRegisterArr);
            statist.Add("numberUser", numberUserRegisterArr);

            return Ok(statist);
        }

        [Route("/api/admin/statist/day/{month}")]
        [HttpGet]
        public ActionResult<Dictionary<string, List<QuantityStatistics>>> StatistCarsByDay(int month)
        {
            var numberCarRegister = _carService.StatistCarsByDay(month);
            var numberUserRegister = _userService.StatistUsersByDay(month);

            int days = DateTime.DaysInMonth(DateTime.Now.Year, month);

            int[] numberCarRegisterArr = new int[days];
            int[] numberUserRegisterArr = new int[days];

            for (var i = 0; i < numberCarRegisterArr.Length; i++)
            {
                numberCarRegisterArr[i] = 0;
            }
            for (var i = 0; i < numberCarRegister.Count ; i++)
            {
                numberCarRegisterArr[numberCarRegister[i].Time - 1] = numberCarRegister[i].Count;
            }

            for (var i = 0; i < numberUserRegisterArr.Length; i++)
            {
                numberUserRegisterArr[i] = 0;
            }
            for (var i = 0; i < numberUserRegister.Count ; i++)
            {
                numberUserRegisterArr[numberUserRegister[i].Time - 1] = numberUserRegister[i].Count;
            }

            Dictionary<string, int[]> statist = new Dictionary<string, int[]>();
            statist.Add("numberCars", numberCarRegisterArr);
            statist.Add("numberUser", numberUserRegisterArr);

            return Ok(statist);
        }

        //[Route("/month")]
        //[HttpGet]
        //public ActionResult<Dictionary<string, string>> GetMonth(int month)
        //{
        //    Dictionary<string, List<QuantityStatistics>> statist = new Dictionary<string, List<QuantityStatistics>>();
        //    for (var i = 0; i < length; i++)
        //    {
                
        //    }
        //    statist.Add("numberCars", numberCarRegister);
        //    statist.Add("numberUser", numberUserRegister);

        //    return Ok(statist);
        //}
    }
}