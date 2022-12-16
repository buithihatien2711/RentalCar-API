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
        private readonly IBookingService _bookingService;

        public AdminController(IUserService userService, IMapper mapper, IUploadImgService uploadImgService, ICarService carService, IBookingService bookingService)
        {
            _uploadImgService = uploadImgService;
            _carService = carService;
            _bookingService = bookingService;
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

        [HttpGet("/api/admin/car/{id}")]
        public ActionResult<CarDetailAdminDto> GetCarById(int id)
        {
            var car = _carService.GetCarById(id);
            if(car == null) return NotFound("Car doesn't exist");

            var carImages = _carService.GetImageByCarId(id);
            return Ok(new CarDetailAdminDto()
            {
                Id = car.Id,
                Name = car.Name,
                Plate_number = car.Plate_number,
                Description = car.Description,
                Capacity = car.Capacity,
                Cost = car.Cost,
                CarBrand = _mapper.Map<CarBrand,CarBrandDto>(car.CarModel.CarBrand),
                CarModel = _mapper.Map<CarModel,CarModelDto>(car.CarModel),
                TransmissionDto = new TransmissionDto()
                {
                    Id = car.TransmissionID,
                    Name = car.Transmission.Name
                },
                FuelTypeDto = new FuelTypeDto()
                {
                    Id = car.FuelType.Id,
                    Name = car.FuelType.Name
                },
                FuelConsumption = car.FuelConsumption,

                LocationDto = car.Location == null ? null : new LocationDto()
                {
                    Id = car.LocationId,
                    Address = car.Location.Address
                },
                WardDto = car.Location == null ? null : (car.Location.Ward == null ? null : new WardDto()
                {
                    Id = car.Location.WardId,
                    Name = car.Location.Ward.Name
                }),

                DistrictDto = car.Location == null ? null : (car.Location.Ward == null ? null : (car.Location.Ward.District == null ? null : new DistrictDto()
                {
                    Id = car.Location.Ward.DistrictID,
                    Name = car.Location.Ward.District.Name
                })),

                Rule = car.Rule,
                NumberStar = car.NumberStar,
                CarImageDtos = _mapper.Map<List<CarImage>,List<CarImageDtos>>(carImages),
                Account = _mapper.Map<User, AccountDto>(car.User)
                // CarReviewDtos = carReviewDtos
            });
        }

        [HttpGet("/api/admin/bookings")]
        public ActionResult<BookingViewAdminDto> GetAllBooking()
        {
            var bookings = _bookingService.GetAllBooking();
            if(bookings == null) return Ok(null);

            var bookingDtos = new List<BookingViewAdminDto>();
            foreach (var booking in bookings)
            {
                bookingDtos.Add
                (
                    new BookingViewAdminDto()
                    {
                        Id = booking.Id,
                        RentDate = booking.RentDate,
                        ReturnDate = booking.ReturnDate,
                        Total = booking.Total,
                        Status = booking.Status,
                        CreatedAt = booking.CreatedAt,
                        User = new UserBooking()
                        {
                            Username = booking.User.Username,
                            Fullname = booking.User.Fullname
                        },
                        Car = new CarBooking()
                        {
                            Id = booking.CarId,
                            Name = booking.Car.Name
                        },
                        Location = _mapper.Map<Location, LocationDto>(booking.Location),
                        Ward = _mapper.Map<Ward, WardDto>(booking.Location.Ward),
                        District = _mapper.Map<District, DistrictDto>(booking.Location.Ward.District)
                    }
                );
            }

            return Ok(bookingDtos);
        }
    }
}