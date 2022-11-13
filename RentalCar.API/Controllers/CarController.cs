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
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly ICarModelService _carmodelService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public CarController(ICarService carService,ICarModelService carmodelService,IUserService userService, IMapper mapper)
        {
            _mapper = mapper;
            _carService = carService;
            _carmodelService = carmodelService;
            _userService = userService;
        }

        [HttpGet("listCar")]
        public ActionResult<List<CarViewDto>> Homepage()
        {
            List<Car> ListCar = _carService.GetCars();
            List<CarViewDto> ListCarView = new List<CarViewDto>();

            foreach(var car in ListCar){
                ListCarView.Add(new CarViewDto{
                    Image = _carService.GetImageByCarId(car.Id),
                    Name = car.Name,
                    Plate_number = car.Plate_number,
                    CarBrandDtos = _mapper.Map<CarBrand,CarBrandDto>(car.CarModel.CarBrand),
                    // CarModels = car.CarModel,
                    CarModelDtos = _mapper.Map<CarModel,CarModelDto>(car.CarModel),
                    Color = car.Color,
                    Capacity = car.Capacity,
                    YearManufacture = car.YearManufacture,
                    TransmissionDtos = _mapper.Map<Transmission,TransmissionDto>(car.Transmission),
                    FuelTypeDtos = _mapper.Map<FuelType,FuelTypeDto>(car.FuelType),
                    FuelConsumption = car.FuelConsumption,
                    Description = car.Description,
                    Cost = car.Cost,
                    AddressCar = _mapper.Map<Location,LocationDto>(car.Location),
                    numberStar = car.numberStar,
                    Rule = car.Rule,
                });
            }
            return Ok(ListCarView);
            // ImageAvt = _CarService.GetImageAvtByCarId()
        }

        [HttpGet("selfdrive")]
        public ActionResult<CarAddDto> CarViewAdd()
        {
            var CarAdd = new CarAddDto{
                CarBrands = _mapper.Map<List<CarBrand>,List<CarBrandDto>>(_carService.GetCarBrands()),
                CarModels =  _mapper.Map<List<CarModel>,List<CarModelDto>>(_carmodelService.GetCarModels()),
                Districts = _carService.GetDistricts(),
                Wards = _mapper.Map<List<Ward>,List<WardDto>>(_carService.GetWards()),
                // Capacity = new List<int>(){1,2,3,4,5,6,7,8,9,10,11,12,13,14},
                // YearManufacture = new List<int>(){2010,2011,2012,2013,2014,2015,2016,2017,2018,2019,2020,2021,2022},
                Transmissions = _mapper.Map<List<Transmission>,List<TransmissionDto>>(_carService.GetTransmissions()),
                FuelTypes = _mapper.Map<List<FuelType>,List<FuelTypeDto>>(_carService.GetFuelTypes())
            };
            return Ok(CarAdd);
        }

        [Authorize(Roles="lease")]
        [HttpPost("selfdrive")]
        public ActionResult<string> AddCar(CarAddInfo car)
        {
            try
            {
                var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _userService.GetUserByUsername(username);
            var location = new Location{
                Address = car.Address,
                WardId = car.WardId,
                UserId = user.Id
            };

            if(_carService.CreateLocation(location) == true){
                _carService.SaveChanges();
            };
            
            if(_carService.GetCarByPateNumber(car.Plate_number) != null){
                throw new UnauthorizedAccessException("Plate number is invalid.");
            }
            var carAdd = new Car{
                Name = car.Name,
                Description = car.Description,
                // Color = car.Color,
                Capacity = car.Capacity,
                Plate_number = car.Plate_number,
                Cost = car.Cost,
                CreatedAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                CarModelId = car.CarModelId,
                StatusID = 1,
                UserId = user.Id,
                YearManufacture = car.YearManufacture,
                TransmissionID = car.TransmissionId,
                FuelTypeID = car.FuelTypeId,
                FuelConsumption = car.FuelConsumption,
                Rule = car.Rule,
                // numberStar = car.numberStar,
                LocationId = _carService.GetLocationByAddress(car.Address).Id
            };

            _carService.CreateCar(carAdd);
            _carService.SaveChanges();
            var Car = _carService.GetCarByPateNumber(car.Plate_number);

            int CarId = Car.Id;
            _carService.InsertImage(CarId,car.Image);
            _carService.SaveChanges();
            return Ok("add thành công");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            
        }
    }
}