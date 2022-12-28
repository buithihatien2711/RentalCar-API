using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalCar.API.Mapping;
using RentalCar.API.Models;
using RentalCar.Data.Repositories;
using RentalCar.Model.Models;
using RentalCar.Service;
using RentalCar_API.RentalCar.Service;

namespace RentalCar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly ICarModelService _carmodelService;
        private readonly IUserService _userService;
        private readonly IUploadImgService _uploadImgService;
        private readonly IMapper _mapper;
        private readonly INotificationService _notifiService;

        public CarController(ICarService carService,ICarModelService carmodelService,IUserService userService, IUploadImgService uploadImgService,IMapper mapper,INotificationService notifiService)
        {
            _mapper = mapper;
            _notifiService = notifiService;
            _carService = carService;
            _carmodelService = carmodelService;
            _uploadImgService = uploadImgService;
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
            for(int i =4; i<8; i++){
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


        [Authorize(Roles="lease, admin")]
        [HttpPost("CarAdd")]
        public async Task<ActionResult<string>> AddCar([FromForm] CarAddInfo car)
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
                    MessageReturn fail = new MessageReturn()
                    {
                        StatusCode = enumMessage.Fail,
                        Message = "Biển số xe đã tồn tại."
                    };
                    return Ok(fail);
                    // Dictionary<string, string> message = new Dictionary<string, string>();
                    // message.Add("Message", "Plate number is invalid.");
                    // return NotFound(message);
                }
                var carAdd = new Car{
                    Name = _carmodelService.GetCarModelById(car.CarModelId).CarBrand.Name + " "
                            +_carmodelService.GetCarModelById(car.CarModelId).Name + " "
                            + car.YearManufacture,
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
                if(car.Image !=null){
                    foreach(var image in car.Image){
                        var linkImage = await _uploadImgService.UploadImage("car",username,image);
                        _carService.InsertImage(CarId,linkImage);
                        _carService.SaveChanges();
                    }
                }
                var result = _carService.GetCarById(CarId);
                var caradd = _mapper.Map<Car,CarViewDto>(result);
                caradd.CarModelDtos = _mapper.Map<CarModel,CarModelDto>(result.CarModel);
                caradd.TransmissionDtos = _mapper.Map<Transmission,TransmissionDto>(result.Transmission);
                caradd.FuelTypeDtos = _mapper.Map<FuelType,FuelTypeDto>(result.FuelType);
                caradd.ImageDtos = _mapper.Map<List<CarImage>,List<CarImageDtos>>(result.CarImages);
                caradd.LocationDto = _mapper.Map<Location,LocationDto>(result.Location);
                caradd.District = _mapper.Map<District,DistrictDto>(result.Location.Ward.District);
                caradd.Ward = _mapper.Map<Ward,WardDto>(result.Location.Ward);
                caradd.Username = username;

                // var admin = _userService.GetUserByUsername("admin");
                // _notifiService.CreateINotifi(new Notification{
                //     FromUserId = user.Id,
                //     ToUserId = admin.Id,
                //     CreateAt = DateTime.Now,
                //     Status = false,
                //     Title = "Kiểm duyệt xe",
                //     Message = caradd.Name + " đang chờ phê duyệt"
                // });

                var Tousers = _userService.GetUsersByRole(1);
                    List<User> admin = new List<User>();
                    foreach(var touser in Tousers){
                        // admin.Add(touser);
                        _notifiService.CreateINotifi(new Notification{
                            FromUserId = user.Id,
                            ToUserId = touser.Id,
                            CreateAt = DateTime.Now,
                            Status = false,
                            Title = "Kiểm duyệt xe",
                            Message = caradd.Name + " đang chờ phê duyệt"
                        });
                    }
                MessageReturn success = new MessageReturn()
                {
                    StatusCode = enumMessage.Success,
                    Message = "Xe bạn đang được chờ phê duyệt."
                };
                return Ok(success);
                // return Ok(caradd);
                }
            catch (Exception ex)
            {
                MessageReturn fail = new MessageReturn()
                {
                    StatusCode = enumMessage.Fail,
                    Message = "Thêm xe thất bại."
                };
                return Ok(fail);
            }
            
        }


        //admin có thể thêm hoặc từ chối xe
        [Authorize(Roles="admin")]
        [HttpPut("/admin/car/{CarID}/approval/{StatusID}")]
        public ActionResult<string> AddCar(int CarId, int StatusID){
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _userService.GetUserByUsername(username);
            _carService.UpdateStatusOfCar(CarId,StatusID);
            var car = _carService.GetCarById(CarId);
            if(_carService.SaveChanges()){
                _notifiService.CreateINotifi(new Notification{
                        FromUserId = user.Id,
                        ToUserId = car.User.Id,
                        CreateAt = DateTime.Now,
                        Status = false,
                        Title = "Kiểm duyệt xe",
                        Message = "Xe "+car.Name + "-" + car.Status
                    });
            }
            // return Ok("admin update status car successful");
            MessageReturn success = new MessageReturn()
            {
                StatusCode = enumMessage.Success,
                Message = "Admin đã xử lí xe."
            };
            return Ok(success);
        }
        [HttpGet("{id}")]
        public ActionResult<CarDetailDto> Get(int id)
        {
            var car = _carService.GetCarById(id);
            if(car == null) return NotFound("Car doesn't exist");

            var carImages = _carService.GetImageByCarId(id);
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
                    Id = car.Location.WardId.Value,
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
            if(_carService.SaveChanges())
            {
                MessageReturn success = new MessageReturn()
                {
                    StatusCode = enumMessage.Success,
                    Message = "Xóa xe thành công"
                };
                return Ok(success);
                // return NoContent();
            } 
            MessageReturn error = new MessageReturn()
            {
                StatusCode = enumMessage.Fail,
                Message = "Xóa xe thất bại"
            };
            return Ok(error);
            // return BadRequest();
        }

        [Authorize(Roles="lease, admin")]
        [HttpGet("mycar/{idStatus}")]
        public ActionResult<List<MyCarOverview>> GetMyCar(int idStatus)
        {
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if(string.IsNullOrEmpty(username)) return NotFound();

            var idUser = _userService.GetUserByUsername(username).Id;

            var cars = _carService.GetCarsByUserAndStatus(idUser, idStatus);
            if(cars == null) return Ok(null);

            var carOverviews = new List<MyCarOverview>();

            foreach (var car in cars)
            {
                carOverviews.Add(new MyCarOverview()
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
                        Id = car.Location.WardId.Value,
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
                Id = car.Id,
                Name = car.Name,
                Status =  _mapper.Map<Status, StatusDto>(car.Status),
                Cost = car.Cost,
                Plate_number = car.Plate_number,
                Address = _mapper.Map<Location,LocationDto>(car.Location).Address,
                WardId = _mapper.Map<Ward,WardDto>(car.Location.Ward).Id,
                DistrictId = car.Location.Ward.District.Id,
                Capacity = car.Capacity,
                Transmission = _mapper.Map<Transmission,TransmissionDto>(car.Transmission),
                FuelType = _mapper.Map<FuelType,FuelTypeDto>(car.FuelType),
                FuelConsumption = car.FuelConsumption,
                Description = car.Description,
                carImages = _mapper.Map<List<CarImage>,List<CarImageDtos>>(_carService.GetImageByCarId(id)),
            };

            return Ok(carInfoView);
        }

        [Authorize(Roles="lease, admin")]
        [HttpPut("{id}/CarInfor")]
        public ActionResult<string> UpdateCarInfor(int id,CarInfoView_UpdateDto carInput)
        {
            try{
                var carSer = _carService.GetCarById(id);
                var ward = _carService.GetWardById(carInput.WardId);
                if(carSer == null) {
                    MessageReturn fail = new MessageReturn()
                    {
                        StatusCode = enumMessage.Fail,
                        Message = "Không tìm thấy xe."
                    };
                    return Ok(fail);
                }
                if(ward == null) {
                    MessageReturn fail = new MessageReturn()
                    {
                        StatusCode = enumMessage.Fail,
                        Message = "Không tìm thấy ward."
                    };
                    return Ok(fail);
                }
                var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var userId = _userService.GetUserByUsername(username).Id;

                var location = new Location{
                        Address = carInput.Address,
                        WardId = carInput.WardId,
                        UserId = userId
                    };
                _carService.UpdateCarInfor(id,location,carInput.FuelConsumption,carInput.Description,carInput.Cost);
                var car = _carService.GetCarById(id);
                if(car == null) return NotFound();
                var carInfoView = new CarInfoView_UpdateDto(){
                    Name = car.Name,
                    Status =  _mapper.Map<Status, StatusDto>(car.Status),
                    Cost = car.Cost,
                    Plate_number = car.Plate_number,
                    Address = _mapper.Map<Location,LocationDto>(car.Location).Address,
                    WardId = _mapper.Map<Ward,WardDto>(car.Location.Ward).Id,
                    DistrictId = car.Location.Ward.District.Id,
                    Capacity = car.Capacity,
                    Transmission = _mapper.Map<Transmission,TransmissionDto>(car.Transmission),
                    FuelType = _mapper.Map<FuelType,FuelTypeDto>(car.FuelType),
                    FuelConsumption = car.FuelConsumption,
                    Description = car.Description,
                    carImages = _mapper.Map<List<CarImage>,List<CarImageDtos>>(_carService.GetImageByCarId(id)),
                };
            MessageReturn success = new MessageReturn()
            {
                StatusCode = enumMessage.Success,
                Message = "Cập nhật thông tin xe thành công."
            };
            return Ok(success);
                // return Ok(_mapper.Map<Car, CarViewDto>(_carService.GetCarById(id)));
            }
            catch(Exception ex){
                MessageReturn fail = new MessageReturn()
                {
                    StatusCode = enumMessage.Fail,
                    Message = "Cập nhật thông tin xe thất bại."
                };
                return Ok(fail);
            }
        }
        
        [HttpGet("{id}/CarImage")]
        public ActionResult<string> ViewCarImage(int id)
        {
            var car = _carService.GetCarById(id);
            if(car == null){
                MessageReturn fail = new MessageReturn()
                    {
                        StatusCode = enumMessage.Fail,
                        Message = "Không tìm thấy xe."
                    };
                    return Ok(fail);
            }

            var CarImage = _mapper.Map<List<CarImage>,List<CarImageDtos>>(_carService.GetImageByCarId(id));

            return Ok(CarImage);
        }

        [Authorize(Roles="lease, admin")]
        [HttpPost("{id}/CarImage")]
        public async Task<ActionResult<string>> AddCarImage(List<IFormFile> listImage,int id)
        {
            try{
                var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var car = _carService.GetCarById(id);
                if(car == null){
                    MessageReturn fail = new MessageReturn()
                    {
                        StatusCode = enumMessage.Fail,
                        Message = "Không tìm thấy xe."
                    };
                    return Ok(fail);
                } 
                foreach(var image in listImage){
                    var linkImage = await _uploadImgService.UploadImage("car",username,image);
                    _carService.InsertImage(id,linkImage);
                    _carService.SaveChanges();
                }
                // List<CarImageDtos> images = _mapper.Map<List<CarImage>,List<CarImageDtos>>(_carService.GetImageByCarId(id));
                // return Ok(images);
                MessageReturn success = new MessageReturn()
                {
                    StatusCode = enumMessage.Success,
                    Message = "Thêm ảnh thành công."
                };
                return Ok(success);
            }
            catch(Exception ex){
                MessageReturn fail = new MessageReturn()
                {
                    StatusCode = enumMessage.Fail,
                    Message = "Thêm ảnh thất bại"
                };
                return Ok(fail);;
                // return BadRequest(ex.Message);
            }
        }

        [HttpDelete("CarImage/{ImgId}")]
        public ActionResult<string> DeleteCarImage(int ImgId)
        {
                _carService.DeleteCarImagebyId(ImgId);
                if(_carService.SaveChanges()) {
                    MessageReturn success = new MessageReturn()
                    {
                        StatusCode = enumMessage.Success,
                        Message = "Xóa ảnh thành công"
                    };
                    return Ok(success);
                }
                // return BadRequest();
                MessageReturn fail = new MessageReturn()
                {
                    StatusCode = enumMessage.Fail,
                    Message = "Xóa ảnh thất bại"
                };
                return Ok(fail);
        }

        // [HttpGet("{id}/CarImageRegister")]
        // public ActionResult<string> ViewCarImgRegister(int id)
        // {
        //     if (_carService.GetCarById(id) == null){
        //         Dictionary<string, string> message = new Dictionary<string, string>();
        //         message.Add("Message", "Delete Image successfull");
        //         return NotFound("Car doesn't exist");
        //     } 
        //     var CarTypeRegisters = _carService.GetCarTypeRegister();
        //     List<CarRegisterDto> CarRegister = new List<CarRegisterDto>();
        //     foreach(var carType in CarTypeRegisters){
        //         CarRegister.Add(new CarRegisterDto{
        //             IdType = carType.Id,
        //             NameType = carType.Name,
        //             listImage = _mapper.Map<List<CarImgRegister>,List<CarImageDtos>>
        //                         (_carService.GetCarImgRegistersByCarIdAndTypeId(id,1) == null 
        //                         ? null : _carService.GetCarImgRegistersByCarIdAndTypeId(id,carType.Id))
        //             });
        //     }
        //     return Ok(CarRegister);
        // }

        // [HttpPost("{id}/CarImageRegister")]
        // public async Task<ActionResult<string>> AddCarImgRegister(int id,[FromForm]List<ImageTypeRegister> imageTypes)
        // {
        //     var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //     try{
        //         var car = _carService.GetCarById(id);
        //         if(car == null){
        //             return NotFound("Car doesn't exist");
        //         } 
        //         foreach(var imageType in imageTypes){
        //             if(imageType.Path !=null){
        //                 List<string> listImage = new List<string>();
        //                 foreach(var image in imageType.Path){
        //                     var linkImage = await _uploadImgService.UploadImage("CarRegister",username,image);
        //                     listImage.Add(linkImage);
        //                 }
        //                 _carService.InsertImageRegister(id,imageType.IdType,listImage);
        //             }
        //         // _carService.SaveChanges();
        //         }
        //         var CarTypeRegisters = _carService.GetCarTypeRegister();
        //         List<CarRegisterDto> CarRegister = new List<CarRegisterDto>();
        //         foreach(var carType in CarTypeRegisters){
        //             CarRegister.Add(new CarRegisterDto{
        //                 IdType = carType.Id,
        //                 NameType = carType.Name,
        //                 listImage = _mapper.Map<List<CarImgRegister>,List<CarImageDtos>>
        //                             (_carService.GetCarImgRegistersByCarIdAndTypeId(id,carType.Id) == null 
        //                             ? null : _carService.GetCarImgRegistersByCarIdAndTypeId(id,carType.Id))
        //                 });
        //     }
        //     return Ok(CarRegister);
                
        //     }
        //     catch(Exception ex){
        //         return BadRequest(ex.Message);
        //     }
        // }

        [Authorize(Roles="lease, admin")]
        [HttpGet("{id}/CarImageType/{idType}")]
        public ActionResult<string> ViewCarImgRegister(int id,int idType)
        {
            var listImage = _mapper.Map<List<CarImgRegister>,List<CarImageDtos>>
                                (_carService.GetCarImgRegistersByCarIdAndTypeId(id,idType) == null 
                                ? null : _carService.GetCarImgRegistersByCarIdAndTypeId(id,idType));
            return Ok(listImage);
        }

        [Authorize(Roles="lease, admin")]
        [HttpPost("{id}/CarImageType/{idType}")]
        public async Task<ActionResult<string>> AddCarImgRegister(int id,int idType,[FromForm]List<IFormFile> images)
        {
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            try{
                var car = _carService.GetCarById(id);
                if(car == null){
                    MessageReturn fail = new MessageReturn()
                    {
                        StatusCode = enumMessage.Fail,
                        Message = "Không tìm thấy xe."
                    };
                    return Ok(fail);
                } 
                if(images !=null){
                    foreach(var image in images){
                        var linkImage = await _uploadImgService.UploadImage("CarRegister",username,image);
                        _carService.InsertImageRegister(id,idType,linkImage);
                    }
                }
                var CarRegister =_carService.GetCarImgRegistersByCarIdAndTypeId(id,idType);
                // return Ok(_mapper.Map<List<CarImgRegister>,List<CarImageDtos>>(CarRegister));
                MessageReturn success = new MessageReturn()
                {
                    StatusCode = enumMessage.Success,
                    Message = "Thêm ảnh thành công."
                };
                return Ok(success);
                
            }
            catch(Exception ex){
                MessageReturn fail = new MessageReturn()
                {
                    StatusCode = enumMessage.Fail,
                    Message = "Thêm ảnh thất bại."
                };
                return Ok(fail);
            }
        }
       
       [Authorize(Roles="lease, admin")]
       [HttpDelete("CarImageRegister/{ImgId}")]
        public ActionResult<string> DeleteCarImageRegister(int ImgId)
        {
                _carService.DeleteCarImageRgtbyId(ImgId);
                if(_carService.SaveChanges()) {
                    MessageReturn success = new MessageReturn()
                    {
                        StatusCode = enumMessage.Success,
                        Message = "Xóa ảnh thành công"
                    };
                    return Ok(success);
                }
                MessageReturn fail = new MessageReturn()
                    {
                        StatusCode = enumMessage.Fail,
                        Message = "Xóa ảnh thất bại"
                    };
                    return Ok(fail);
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

        [HttpGet("find")]
        public ActionResult<List<CarFindDto>> ViewCarImgRegister([FromQuery]SearchParam searchParam)
        {
            List<Car>? cars = _carService.GetCarsFilterSort(searchParam);
            if (cars == null) return Ok(null);
            return Ok(_mapper.Map<List<Car>, List<CarFindDto>>(cars));
        }

        [HttpGet("/api/Car/sortby")]
        public ActionResult<Dictionary<string, SortByDto[]>> GetSortByParam()
        {
            
            SortByDto price_asc = new SortByDto(){Id = 1, Nickname = "price_asc", Name = "Giá tăng dần"};
            SortByDto price_desc = new SortByDto(){Id = 2, Nickname = "price_desc", Name = "Giá giảm dần" };
            SortByDto rate_desc = new SortByDto(){Id = 3, Nickname = "rate_desc", Name = "Đánh giá" };

            Dictionary<string, SortByDto[]> sortBy = new Dictionary<string, SortByDto[]>();
            sortBy.Add("sortby", new SortByDto[]{price_asc, price_desc, rate_desc});

            return Ok(sortBy);
        }

        [HttpGet("user/{idUser}")]
        public ActionResult<string> GetCarByUser(int idUser)
        {
            if(_userService.GetUserById(idUser) == null) return NotFound();
            var cars = _carService.GetCarsByUser(idUser);
            List<CarViewDto> listCarView = new List<CarViewDto>();

            if(cars == null) return Ok(null);

            foreach(var car in cars){
                listCarView.Add(new CarViewDto{
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
            return Ok(listCarView);
        }

        // Admin statist car by month in year
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

        // Admin statist car by day in month
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

        // Admin get car detail
        [HttpGet("/api/admin/car/{id}")]
        public ActionResult<CarDetailAdminDto> GetCarById(int id)
        {
            var car = _carService.GetCarById(id);
            if(car == null){
                MessageReturn fail = new MessageReturn()
                    {
                        StatusCode = enumMessage.Fail,
                        Message = "Không tìm thấy xe."
                    };
                    return Ok(fail);
            }

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
                    Id = car.Location.WardId.Value,
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

        
    
    }
}