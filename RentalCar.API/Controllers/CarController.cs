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

        [HttpGet("allCars")]
        public ActionResult<List<CarViewDto>> listCar()
        {
            List<Car> ListCar = _carService.GetCars();
            List<CarViewDto> ListCarView = new List<CarViewDto>();

            foreach(var car in ListCar){
                ListCarView.Add(new CarViewDto{
                    Id = car.Id,
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
                    numberStar = car.NumberStar,
                    Rule = car.Rule,
                    Status = _mapper.Map<Status,StatusDto>(car.Status),
                    LocationDto = _mapper.Map<Location,LocationDto>(car.Location),
                    Ward = _mapper.Map<Ward,WardDto>(car.Location.Ward),
                    District = _mapper.Map<District,DistrictDto>(car.Location.Ward.District),
                    Username = car.User.Username
                });
            }
            return Ok(ListCarView);
            // ImageAvt = _CarService.GetImageAvtByCarId()
        }

        [HttpGet("carsActive")]
        public ActionResult<List<CarViewDto>> listCarActive()
        {
            List<Car> ListCar = _carService.GetCarsByStatus(3);
            List<CarViewDto> ListCarView = new List<CarViewDto>();

            foreach(var car in ListCar){
                ListCarView.Add(new CarViewDto{
                    Id = car.Id,
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
                    numberStar = car.NumberStar,
                    Rule = car.Rule,
                    Status = _mapper.Map<Status,StatusDto>(car.Status),
                    LocationDto = _mapper.Map<Location,LocationDto>(car.Location),
                    Ward = _mapper.Map<Ward,WardDto>(car.Location.Ward),
                    District = _mapper.Map<District,DistrictDto>(car.Location.Ward.District),
                    Username = car.User.Username
                });
            }
            return Ok(ListCarView);
            // ImageAvt = _CarService.GetImageAvtByCarId()
        }

        [HttpGet("CarMoreInfor")]
        public ActionResult<CarAddDto> CarViewAdd()
        {
            List<CapacityDto> capacities = new List<CapacityDto>();
            for(int i =1; i<5; i++){
                capacities.Add(new CapacityDto{
                    Id = i,
                    Capacity = i
                });
            }
            List<YearManufactureDto> yearManufactures = new List<YearManufactureDto>();
            for(int i =2015; i<2022; i++){
                yearManufactures.Add(new YearManufactureDto{
                    Id = i,
                    Year = i
                });
            }
            var CarAdd = new CarAddDto{
                Capacities = capacities,
                YearManufactures = yearManufactures,
                Transmissions = _mapper.Map<List<Transmission>,List<TransmissionDto>>(_carService.GetTransmissions()),
                FuelTypes = _mapper.Map<List<FuelType>,List<FuelTypeDto>>(_carService.GetFuelTypes()),
                
            };
            return Ok(CarAdd);
        }
        [HttpGet("/api/Brand")]
        public ActionResult<District> Brands()
        {
            var Brands = _mapper.Map<List<CarBrand>,List<CarBrandDto>>(_carService.GetCarBrands());
            return Ok(Brands);
        }

        [HttpGet("/api/Brand/{Id}")]
        public ActionResult<CarModelDto> ModelByBrand(int Id)
        {
            List<CarModelDto> wards = _mapper.Map<List<CarModel>,List<CarModelDto>>(_carmodelService.GetCarModelsByCarBrandId(Id));
            return Ok(wards);
        }
        [HttpGet("/api/District")]
        public ActionResult<District> Districts()
        {
            var Districts = _carService.GetDistricts();
            return Ok(Districts);
        }

        [HttpGet("/api/District/{Id}")]
        public ActionResult<WardDto> WardByDistrict(int Id)
        {
            List<WardDto> wards = _mapper.Map<List<Ward>,List<WardDto>>(_carService.GetWardsByDictrictsId(Id));
            return Ok(wards);
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
                    return NotFound("Plate number is invalid.");
                    // Dictionary<string, string> message = new Dictionary<string, string>();
                    // message.Add("Message", "Plate number is invalid.");
                    // return NotFound(message);
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }


        //admin có thể thêm hoặc từ chối xe
        [Authorize(Roles="admin")]
        [HttpPut("/admin/car/{CarID}/approval/{StatusID}")]
        public ActionResult<string> AddCar(int CarId, int StatusID){
            _carService.UpdateStatusOfCar(CarId,StatusID);
            _carService.SaveChanges();
            return Ok("admin update status car successful");
            // Dictionary<string, string> message = new Dictionary<string, string>();
            // message.Add("Message", "admin update status car successful");
            // return Ok(message);
        }
        [HttpGet("{Id}")]
        public ActionResult<CarDetailDto> Get(int Id)
        {
            var car = _carService.GetCarById(Id);
            if(car == null) return NotFound("Car doesn't exist");

            var carImages = _carService.GetImageByCarId(Id);
            // var carReviews = _carService.GetCarReviewsByCarId(id);
            // var carReviewDtos = new List<CarReviewDto>();

            // if(carReviews != null)
            // {
            //     foreach (var carReview in carReviews)
            //     {
            //         carReviewDtos.Add(new CarReviewDto()
            //         {
            //             Id = carReview.Id,
            //             Content = carReview.Content,
            //             Rating = carReview.Rating,
            //             CreatedAt = carReview.CreatedAt,
            //             UpdateAt = carReview.UpdateAt,
            //             AccountDto = car.User == null ? null : new AccountDto()
            //             {
            //                 ProfileImage = carReview.User.ProfileImage,
            //                 Fullname = carReview.User.Fullname
            //             }
            //         });
            //     }
            // } 

            return Ok(new CarDetailDto()
            {
                Id = car.Id,
                Name = car.Name,
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
                Account = _mapper.Map<User, AccountDto>(car.User)
                // CarReviewDtos = carReviewDtos
            });
        }

        [Authorize(Roles = "lease, admin")]
        [HttpDelete("{id}")]
        public ActionResult<CarDetailDto> Delete(int id)
        {
            var car = _carService.GetCarById(id);
            if(car == null) return NotFound();

            _carService.DeleteCar(car);
            if(_carService.SaveChanges()) return NoContent();
            return BadRequest();
        }

        [Authorize(Roles="lease")]
        [HttpGet("mycar/{idStatus}")]
        public ActionResult<List<CarOverview>> GetMyCar(int idStatus)
        {
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if(string.IsNullOrEmpty(username)) return NotFound();

            var idUser = _userService.GetUserByUsername(username).Id;

            var cars = _carService.GetCarsByUserAndStatus(idUser, idStatus);
            if(cars == null) return Ok(null);

            var carOverviews = new List<CarOverview>();

            foreach (var car in cars)
            {
                carOverviews.Add(new CarOverview()
                {
                    Id = car.Id,
                    Name = car.Name,
                    Cost = car.Cost,
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
                    Status = car.Status.Name,
                    Image = car.CarImages == null ? null : car.CarImages[0].Path
                    });
            }

            return Ok(carOverviews);
        }

        [HttpGet("{id}/CarInfor")]
        public ActionResult<CarInfoView_UpdateDto> ViewCarInfor(int id)
        {
            var car = _carService.GetCarById(id);
            if(car == null) return NotFound();

            var carInfoView = new CarInfoView_UpdateDto(){
                Name = car.Name,
                Status =  _mapper.Map<Status, StatusDto>(car.Status),
                Cost = car.Cost,
                Plate_number = car.Plate_number,
                location = _mapper.Map<Location,LocationDto>(car.Location),
                Ward = _mapper.Map<Ward,WardDto>(car.Location.Ward),
                District = car.Location.Ward.District,
                Capacity = car.Capacity,
                Transmission = _mapper.Map<Transmission,TransmissionDto>(car.Transmission),
                FuelType = _mapper.Map<FuelType,FuelTypeDto>(car.FuelType),
                FuelConsumption = car.FuelConsumption,
                Description = car.Description,
                carImages = _mapper.Map<List<CarImage>,List<CarImageDtos>>(_carService.GetImageByCarId(id)),
            };

            return Ok(carInfoView);
        }
        [HttpPut("CarInfor")]
        public ActionResult<string> UpdateCarInfor(CarInfo_UpdateDto carInput)
        {
            try{
                var car = _carService.GetCarById(carInput.Id);
                var ward = _carService.GetWardById(carInput.WardId);
                if(car == null) {
                    return NotFound("Car doesn't exist");
                }
                if(ward == null) {
                    return NotFound("Ward doesn't exist");
                }
                var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var user = _userService.GetUserByUsername(username);

                var location = new Location{
                        Address = carInput.Address,
                        WardId = carInput.WardId,
                        UserId = user.Id
                    };
                _carService.UpdateCarInfor(car.Id,location,carInput.FuelConsumption,carInput.Description,carInput.Cost);
                return Ok(carInput);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("{id}/CarImage")]
        public ActionResult<string> ViewCarImage(int id)
        {
            var car = _carService.GetCarById(id);
            if(car == null){
                return NotFound("Car doesn't exist");
            }

            var CarImage = _mapper.Map<List<CarImage>,List<CarImageDtos>>(_carService.GetImageByCarId(id));

            return Ok(CarImage);
        }

        [HttpPut("{id}/CarImage")]
        public ActionResult<string> AddCarImage(List<string> listImage,int id)
        {
            try{
                var car = _carService.GetCarById(id);
                if(car == null){
                    return NotFound("Car doesn't exist");
                } 
                _carService.InsertImage(id,listImage);
                _carService.SaveChanges();
                List<CarImageDtos> images = _mapper.Map<List<CarImage>,List<CarImageDtos>>(_carService.GetImageByCarId(id));
                return Ok(images);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("CarImage/{ImgId}")]
        public ActionResult<string> DeleteCarImage(int ImgId)
        {
                _carService.DeleteCarImagebyId(ImgId);
                if(_carService.SaveChanges()) {
                    return NoContent();
                }
                return BadRequest();
        }

        [HttpGet("{id}/CarImageRegister")]
        public ActionResult<string> ViewCarImgRegister(int id)
        {
            if (_carService.GetCarById(id) == null){
                Dictionary<string, string> message = new Dictionary<string, string>();
                message.Add("Message", "Delete Image successfull");
                return NotFound("Car doesn't exist");
            } 
            var CarTypeRegisters = _carService.GetCarTypeRegister();
            List<CarRegisterDto> CarRegister = new List<CarRegisterDto>();
            foreach(var carType in CarTypeRegisters){
                CarRegister.Add(new CarRegisterDto{
                    IdType = carType.Id,
                    NameType = carType.Name,
                    listImage = _mapper.Map<List<CarImgRegister>,List<CarImageDtos>>
                                (_carService.GetCarImgRegistersByCarIdAndTypeId(id,1) == null 
                                ? null : _carService.GetCarImgRegistersByCarIdAndTypeId(id,carType.Id))
                    });
            }
            return Ok(CarRegister);
        }

        [HttpPost("{id}/CarImageRegister")]
        public ActionResult<string> AddCarImgRegister(int id,List<ImageTypeRegister> imageTypes)
        {
            try{
                var car = _carService.GetCarById(id);
                if(car == null){
                    return NotFound("Car doesn't exist");
                } 
                foreach(var imageType in imageTypes){
                _carService.InsertImageRegister(id,imageType.IdType,imageType.Path);
                // _carService.SaveChanges();
                }
                var CarTypeRegisters = _carService.GetCarTypeRegister();
                List<CarRegisterDto> CarRegister = new List<CarRegisterDto>();
                foreach(var carType in CarTypeRegisters){
                    CarRegister.Add(new CarRegisterDto{
                        IdType = carType.Id,
                        NameType = carType.Name,
                        listImage = _mapper.Map<List<CarImgRegister>,List<CarImageDtos>>
                                    (_carService.GetCarImgRegistersByCarIdAndTypeId(id,carType.Id) == null 
                                    ? null : _carService.GetCarImgRegistersByCarIdAndTypeId(id,carType.Id))
                        });
            }
            return Ok(CarRegister);
                
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
       
       [HttpDelete("CarImageRegister/{ImgId}")]
        public ActionResult<string> DeleteCarImageRegister(int ImgId)
        {
                _carService.DeleteCarImageRgtbyId(ImgId);
                if(_carService.SaveChanges()) {
                    return NoContent();
                }
                return BadRequest();
        }

        [HttpGet("cars/{idStatus}")]
        public ActionResult<List<CarViewAdminDto>>? GetListCarByStatus(int idStatus)
        {
            List<Car>? listCar = _carService.GetCarsByStatus(idStatus);
            if(listCar == null) return null;

            List<CarViewAdminDto> listCarViews = new List<CarViewAdminDto>();
            foreach (var car in listCar)
            {
                List<CarRegisterDto> carRegisterDtos = new List<CarRegisterDto>();
                
                foreach (var carRegister in car.CarRegisters)
                {
                    carRegisterDtos.Add(new CarRegisterDto()
                    {
                        IdType = carRegister.CarTypeRgtId,
                        NameType = carRegister.CarTypeRgt.Name,
                        listImage = _mapper.Map<List<CarImgRegister>,List<CarImageDtos>>(carRegister.CarImgRegisters)
                    });
                }

                listCarViews.Add(new CarViewAdminDto()
                {
                    Id = car.Id,
                    Name = car.Name,
                    Description = car.Description,
                    Color = car.Color,
                    Capacity = car.Capacity,
                    Plate_number = car.Plate_number,
                    Cost = car.Cost,
                    CreatedAt = car.CreatedAt,
                    UpdateAt = car.UpdateAt,
                    CarModel = _mapper.Map<CarModel, CarModelDto>(car.CarModel),
                    CarBrand = _mapper.Map<CarBrand, CarBrandDto>(car.CarModel.CarBrand),
                    Status = _mapper.Map<Status, CarStatusDto>(car.Status),
                    User = _mapper.Map<User, UserOverviewDto>(car.User),
                    YearManufacture = car.YearManufacture,
                    Rule = car.Rule,
                    NumberStar = car.NumberStar,
                    NumberTrip = car.NumberTrip,
                    CarImages = _mapper.Map<List<CarImage>, List<CarImageDtos>>(car.CarImages),
                    Transmission = _mapper.Map<Transmission, TransmissionDto>(car.Transmission),
                    FuelType = _mapper.Map<FuelType, FuelTypeDto>(car.FuelType),
                    Ward = _mapper.Map<Ward,WardDto>(car.Location.Ward),
                    District = _mapper.Map<District,DistrictDto>(car.Location.Ward.District),
                    Location = _mapper.Map<Location,LocationDto>(car.Location),
                    CarRegisters = carRegisterDtos
                });
            }
            return Ok(listCarViews);
        }
    }
}