using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalCar.API.Mapping;
using RentalCar.API.Models;
using RentalCar.Model.Models;
using RentalCar.Service;

namespace RentalCar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class carregistermodeController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly IUserService _userService;
        private readonly ICarService carService;
        private readonly IMapper _mapper;

        public carregistermodeController(ICarService CarService,IUserService UserService, IMapper mapper)
        {
            _carService = CarService;
            _userService = UserService;
            _mapper = mapper;
        }

        [HttpGet("selfdrive")]
        public ActionResult<AddCarDto> AddCarView()
        {
            var CarAdd = new AddCarDto{
                CarModels =  _carService.GetCarModels(),
                CarBrands = _carService.GetCarBrands(),
                Districts = _carService.GetDistricts(),
                Wards = _carService.GetWards(),
                Capacity = new List<int>(){1,2,3,4,5,6,7,8,9,10,11,12,13,14},
                YearManufacture = new List<int>(){2010,2011,2012,2013,2014,2015,2016,2017,2018,2019,2020,2021,2022},
                Transmission =new List<string>(){"Số tự động","Số sàn"},
                FuelType =new List<string>(){"Xăng","Dầu diesel"}
            };
            return Ok(CarAdd);
        }

        [HttpPost("selfdrive")]
        public ActionResult<string> AddCar(AddCarInfor car)
        {
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _userService.GetUserByUsername(username);
            var carAdd = new Car{
                Name = car.Name,
                Description = car.Description,
                Color = car.Color,
                Capacity = car.Capacity,
                Plate_number = car.Plate_number,
                Cost = car.Cost,
                CreatedAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                CarModelId = car.CarModelId,
                StatusID = 1,
                UserId = user.Id,
                YearManufacture = car.YearManufacture,
                Transmission = car.Transmission,
                FuelType = car.FuelType,
                FuelConsumption = car.FuelConsumption,
                AddressCar = car.AddressCar,
                Rule = car.Rule,
                numberStar = car.numberStar,
            };

            _carService.CreateCar(carAdd);
            _carService.SaveChanges();
            var Car = _carService.GetCarByPateNumber(car.Plate_number);

            int CarId = Car.Id;
            _carService.InsertImage(CarId,car.Image);
            _carService.SaveChanges();
            return Ok("add thành công");
        }
    }
}