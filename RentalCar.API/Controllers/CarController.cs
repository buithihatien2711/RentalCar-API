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
        public ActionResult<List<CarViewDto>> listCar()
        {
            List<Car> ListCar = _carService.GetCars();
            List<CarViewDto> ListCarView = new List<CarViewDto>();

            foreach(var car in ListCar){
                ListCarView.Add(new CarViewDto{
                    ImageDtos = _mapper.Map<List<CarImage>,List<CarImageDtos>>(_carService.GetImageByCarId(car.Id)),
                    Name = car.Name,
                    Plate_number = car.Plate_number,
                    // CarBrandDtos = _mapper.Map<CarBrand,CarBrandDto>(car.CarModel.CarBrand),
                    CarModelDtos = _mapper.Map<CarModel,CarModelDto>(car.CarModel),
                    Color = car.Color,
                    Capacity = car.Capacity,
                    YearManufacture = car.YearManufacture,
                    TransmissionDtos = _mapper.Map<Transmission,TransmissionDto>(car.Transmission),
                    FuelTypeDtos = _mapper.Map<FuelType,FuelTypeDto>(car.FuelType),
                    FuelConsumption = car.FuelConsumption,
                    Description = car.Description,
                    Cost = car.Cost,
                    LocationDto = _mapper.Map<Location,LocationDto>(car.Location),
                    numberStar = car.NumberStar,
                    Rule = car.Rule,
                    Status = _mapper.Map<Status,StatusDto>(car.Status),
                });
            }
            return Ok(ListCarView);
            // ImageAvt = _CarService.GetImageAvtByCarId()
        }

        [HttpGet("ViewCarAdd")]
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
        [HttpPost("CarAdd")]
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
                NumberStar = 0,
                LocationId = _carService.GetLocationByAddress(car.Address).Id
            };

            _carService.CreateCar(carAdd);
            _carService.SaveChanges();
            var Car = _carService.GetCarByPateNumber(car.Plate_number);

            int CarId = Car.Id;
            _carService.InsertImage(CarId,car.Image);
            _carService.SaveChanges();
            var result = _carService.GetCarById(CarId);
            var caradd = _mapper.Map<Car,CarViewDto>(result);
            caradd.CarModelDtos = _mapper.Map<CarModel,CarModelDto>(result.CarModel);
            caradd.TransmissionDtos = _mapper.Map<Transmission,TransmissionDto>(result.Transmission);
            caradd.FuelTypeDtos = _mapper.Map<FuelType,FuelTypeDto>(result.FuelType);
            caradd.ImageDtos = _mapper.Map<List<CarImage>,List<CarImageDtos>>(result.CarImages);
            caradd.LocationDto = _mapper.Map<Location,LocationDto>(result.Location);

            return Ok(caradd);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            
        }

        //admin có thể thêm hoặc từ chối xe
        [Authorize(Roles="admin")]
        [HttpPut("/admin/car/approval")]
        public ActionResult<string> AddCar(int CarId, int StatusID){
            _carService.UpdateStatusOfCar(CarId,StatusID);
            _carService.SaveChanges();
            return("admin update status car successful");
        }

        [HttpGet("{id}")]
        public ActionResult<CarDetailDto> Get(int id)
        {
            var car = _carService.GetCarById(id);
            if(car == null) return NotFound();

            var carImages = _carService.GetImageByCarId(id);
            var carReviews = _carService.GetCarReviewsByCarId(id);
            var carReviewDtos = new List<CarReviewDto>();

            if(carReviews != null)
            {
                foreach (var carReview in carReviews)
                {
                    carReviewDtos.Add(new CarReviewDto()
                    {
                        Id = carReview.Id,
                        Content = carReview.Content,
                        Rating = carReview.Rating,
                        CreatedAt = carReview.CreatedAt,
                        UpdateAt = carReview.UpdateAt,
                        AccountDto = new AccountDto()
                        {
                            ProfileImage = carReview.User.ProfileImage,
                            Fullname = carReview.User.Fullname
                        }
                    });
                }
            } 

            return Ok(new CarDetailDto()
            {
                Id = car.Id,
                Description = car.Description,
                Capacity = car.Capacity,
                Cost = car.Cost,
                CarModel = car.CarModel.Name,
                CarBrand = car.CarModel.CarBrand.Name,
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
                CarReviewDtos = carReviewDtos
            });
        }
    
        [HttpDelete("{id}")]
        public ActionResult<CarDetailDto> Delete(int id)
        {
            var car = _carService.GetCarById(id);
            if(car == null) return NotFound();

            _carService.DeleteCar(car);
            if(_carService.SaveChanges()) return NoContent();
            return BadRequest();
        }

       

    }
}