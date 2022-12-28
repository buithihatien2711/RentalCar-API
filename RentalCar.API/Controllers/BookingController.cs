using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalCar.API.Models;
using RentalCar.Model.Models;
using RentalCar.Service;
using RentalCar.Service.Models;
using RentalCar_API.RentalCar.Service;

namespace RentalCar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly IUserService _userService;
        private readonly IUploadImgService _uploadImgService;
        private readonly IBookingService _bookingService;
        private readonly INotificationService _notifiService;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;

        public BookingController(ICarService carService,IUserService userService,IBookingService bookingService,INotificationService notifiService,IMapper mapper, IPaymentService paymentService)
        {
            _mapper = mapper;
            _paymentService = paymentService;
            _carService = carService;
            _userService = userService;
            _bookingService = bookingService;
            _notifiService = notifiService;
        }


        [HttpGet("/api/Car/{id}/BookingInfor")]
        public ActionResult<string> Get(int id)
        {
            var car = _carService.GetCarById(id);
            var Booking = new BookingDto{
                Cost = car.Cost,
                Schedules = _mapper.Map<List<CarSchedule>,List<CarScheduleDto>>(car.CarSchedules),
                PriceByDates = _mapper.Map<List<PriceByDate>,List<PriceByDateDto>>(car.PriceByDates)
            };

            return Ok(Booking);
        }

        [HttpGet("/api/Car/{id}/PriceAverage")]
        public ActionResult<string> Get(int id, DateTime RentDate, DateTime ReturnDate)
        {
            var car = _carService.GetCarById(id);
            string message = "Thời gian đặt xe hợp lệ";
            decimal price = 0;
            int count = 0;
            for(var day = RentDate ; day <= ReturnDate ; day = day.AddDays(1)){
                count++;
                if(_carService.CheckScheduleByDate(id,day) == true) message = "Xe bận trong khoảng thời gian trên. Vui lòng đặt xe khác hoặc thay đổi lịch trình thích hợp.";
                var resultBefore = price;
                foreach(var priceDate in car.PriceByDates){
                    if(priceDate.Date.Date == day.Date) price += priceDate.Cost;
                }
                price = (price != resultBefore) ? price : resultBefore + car.Cost;
            }
            var result = new BookingPrice{
                Day = count,
                PriceAverage = price/count,
                Total = price,
                Schedule =message
            };

            // var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            // var user = _userService.GetUserByUsername(username);
            // var result = _bookingService.CalculatePriceAverage(id,user,RentDate, ReturnDate);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("/api/Car/{id}/Booking")]
        public ActionResult<string> Post(int id,[FromForm] BookingAddDto booking)
        {
            try{
                var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var user = _userService.GetUserByUsername(username);
                var location = new Location{
                    Address = booking.Address,
                    WardId = booking.WardId,
                    UserId = user.Id
                };

                if(_carService.CreateLocation(location) == true){
                    _carService.SaveChanges();
                };

                var value = _mapper.Map<BookingAddDto,Booking>(booking);
                value.Status = enumStatus.WaitConfirm;
                value.CreatedAt = DateTime.Now;
                value.LocationId = _carService.GetLocationByAddress(booking.Address).Id;
                value.UserId = user.Id;
                value.CarId = id;
                //Total
                 var car = _carService.GetCarById(id);
                decimal price = 0;
                for(var day = booking.RentDate ; day <= booking.ReturnDate ; day = day.AddDays(1)){
                    var resultBefore = price;
                    foreach(var priceDate in car.PriceByDates){
                        if(priceDate.Date.Date == day.Date) price += priceDate.Cost;
                    }
                    price = (price != resultBefore) ? price : resultBefore + car.Cost;
                }
                value.Total = price;

                _bookingService.CreateBooking(value);
                if(_bookingService.SaveChanges()) {
                    var bookingCur = _bookingService.GetCurrentBooking();
                    _notifiService.CreateINotifi(new Notification{
                        FromUserId = user.Id,
                        ToUserId = car.User.Id,
                        CreateAt = DateTime.Now,
                        Status = false,
                        BookingId = bookingCur.Id,
                        Booking = bookingCur,
                        Title = "Đặt xe",
                        Message = user.Username + " gửi yêu cầu đặt xe"
                    });
                    MessageReturn success = new MessageReturn()
                    {
                        StatusCode = enumMessage.Success,
                        Message = "Đã gửi yêu cầu đặt xe thành công"
                    };
                    
                    return Ok(success);
                }
                else{
                    MessageReturn error = new MessageReturn()
                    {
                        StatusCode = enumMessage.Fail,
                        Message = "Gửi yêu cầu đặt xe thất bại"
                    };
                    return Ok(error);
                }
            }
            catch(Exception ex){
                MessageReturn exception = new MessageReturn()
                    {
                        StatusCode = enumMessage.Fail,
                        Message = ex.Message
                    };
                return Ok(exception);
            }
        }

        [Authorize]
        [HttpPut("/api/booking/confirm/{idBooking}")]
        public ActionResult ConfirmBooking(int idBooking)
        {
            _bookingService.ConfirmBooking(idBooking);
            if(_bookingService.SaveChanges())
            {
                var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var user = _userService.GetUserByUsername(username);
                var booking = _bookingService.GetBookingById(idBooking);
                _notifiService.CreateINotifi(new Notification{
                        FromUserId = user.Id,
                        ToUserId = booking.UserId,
                        CreateAt = DateTime.Now,
                        Status = false,
                        BookingId = booking.Id,
                        Booking = booking,
                        Title = "Đặt xe",
                        Message = "Chủ xe đã chấp nhận yêu cầu đặt xe"
                    });
                MessageReturn success = new MessageReturn()
                {
                    StatusCode = enumMessage.Success,
                    Message = "Chấp nhận yêu cầu đặt xe thành công"
                };
                return Ok(success);
            }
            
            MessageReturn error = new MessageReturn()
            {
                StatusCode = enumMessage.Fail,
                Message = "Chấp nhận yêu cầu đặt xe thất bại"
            };
            return Ok(error);
        }

        // [Authorize]
        // [HttpPut("/api/booking/cancelBySystemWaitConfirm/{idBooking}")]
        // public ActionResult CancelBookingBySystemWaitConfirm(int idBooking)
        // {
        //     _bookingService.CancelBySystemWaitConfirm(idBooking);

        //     if(_bookingService.SaveChanges())
        //     {
        //         MessageReturn success = new MessageReturn()
        //         {
        //             StatusCode = enumMessage.Success,
        //             Message = "Hủy đặt xe thành công"
        //         };
        //         return Ok(success);
        //     }
        //     MessageReturn error = new MessageReturn()
        //     {
        //         StatusCode = enumMessage.Fail,
        //         Message = "Hủy đặt xe thất bại"
        //     };
        //     return Ok(error);
        // }
        
        // [Authorize]
        // [HttpPut("/api/booking/cancelBySystemWaitDeposit/{idBooking}")]
        // public ActionResult CancelBookingBySystemWaitDeposit(int idBooking)
        // {
        //     _bookingService.CancelBySystemWaitDeposit(idBooking);

        //     if(_bookingService.SaveChanges())
        //     {
        //         MessageReturn success = new MessageReturn()
        //         {
        //             StatusCode = enumMessage.Success,
        //             Message = "Hủy đặt xe thành công"
        //         };
        //         return Ok(success);
        //     }
        //     MessageReturn error = new MessageReturn()
        //     {
        //         StatusCode = enumMessage.Fail,
        //         Message = "Hủy đặt xe thất bại"
        //     };
        //     return Ok(error);
        // }

        [Authorize]
        [HttpPut("/api/booking/confirmReceivedCar/{idBooking}")]
        public ActionResult ConfirmReceivedCar(int idBooking)
        {
            _bookingService.ConfirmReceivedCar(idBooking);

            if(_bookingService.SaveChanges())
            {
                var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var user = _userService.GetUserByUsername(username);
                var booking = _bookingService.GetBookingById(idBooking);
                _notifiService.CreateINotifi(new Notification{
                    FromUserId = user.Id,
                    ToUserId = booking.Car.UserId,
                    CreateAt = DateTime.Now,
                    Status = false,
                    BookingId = booking.Id,
                    Booking = booking,
                    Title = "Đặt xe",
                    Message = "Người dùng " + user.Username +" đã nhận xe"
                });
                MessageReturn success = new MessageReturn()
                {
                    StatusCode = enumMessage.Success,
                    Message = "Xác nhận nhận xe thành công"
                };
                return Ok(success);
            }
            MessageReturn error = new MessageReturn()
            {
                StatusCode = enumMessage.Fail,
                Message = "Xác nhận nhận xe thất bại"
            };
            return Ok(error);
        }

        [Authorize]
        [HttpPut("/api/booking/confirmCompleteTrip/{idBooking}")]
        public ActionResult ConfirmCompleteTrip(int idBooking)
        {
            _bookingService.ConfirmCompleteTrip(idBooking);

            if(_bookingService.SaveChanges())
            {
                var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var user = _userService.GetUserByUsername(username);
                var booking = _bookingService.GetBookingById(idBooking);
                _notifiService.CreateINotifi(new Notification{
                    FromUserId = user.Id,
                    ToUserId = booking.UserId,
                    CreateAt = DateTime.Now,
                    Status = false,
                    BookingId = booking.Id,
                    Booking = booking,
                    Title = "Đặt xe",
                    Message = "Chủ xe đã xác nhận hoàn thành chuyến"
                });
                MessageReturn success = new MessageReturn()
                {
                    StatusCode = enumMessage.Success,
                    Message = "Xác nhận hoàn thành chuyến thành công"
                };
                return Ok(success);
            }
            MessageReturn error = new MessageReturn()
            {
                StatusCode = enumMessage.Fail,
                Message = "Xác nhận hoàn thành chuyến thất bại"
            };
            return Ok(error);
        }

        [Authorize]
        [HttpPut("/api/booking/cancelBookingByUser/{idBooking}")]
        public ActionResult CancelBookingByUser(int idBooking)
        {
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier);
            if(username == null) return Unauthorized("Please login");

            // _bookingService.CancelByLease(idBooking);
            _bookingService.CancelByUser(idBooking, _userService.GetUserByUsername(username.Value).Id);

            if(_bookingService.SaveChanges())
            {
                var user = _userService.GetUserByUsername(username.Value);
                var booking = _bookingService.GetBookingById(idBooking);
                _notifiService.CreateINotifi(new Notification{
                    FromUserId = user.Id,
                    ToUserId = user.Id == booking.UserId ? booking.Car.UserId : booking.UserId,
                    CreateAt = DateTime.Now,
                    Status = false,
                    BookingId = booking.Id,
                    Booking = booking,
                    Title = "Đặt xe",
                    Message = user.Id == booking.UserId ? 
                            "Người dùng " + user.Username + " hủy chuyến"
                            :"Chủ xe hủy chuyến"
                });
                MessageReturn success = new MessageReturn()
                {
                    StatusCode = enumMessage.Success,
                    Message = "Hủy đặt xe thành công"
                };
                return Ok(success);
            }
            MessageReturn error = new MessageReturn()
            {
                StatusCode = enumMessage.Fail,
                Message = "Hủy đặt xe thất bại"
            };
            return Ok(error);
        }

        // [Authorize]
        // [HttpPut("/api/booking/cancelBookingByRenter/{idBooking}")]
        // public ActionResult CancelBookingByRenter(int idBooking)
        // {
        //     _bookingService.CancelByRenter(idBooking);

        //     if(_bookingService.SaveChanges())
        //     {
        //         var booking = _bookingService.GetBookingById(idBooking);
        //         var bookingDto = new BookingOverviewDto()
        //         {
        //             BookingId = booking.Id,
        //             CarId = booking.CarId,
        //             CarImage = booking.Car.CarImages == null ? null : booking.Car.CarImages[0].Path,
        //             CarName = booking.Car.Name,
        //             RentDate = booking.RentDate,
        //             ReturnDate = booking.ReturnDate,
        //             Total = booking.Total,
        //             Status = new StatusDto()
        //             {
        //                 Id = ((int)booking.Status),
        //                 Name = _bookingService.GetNameStatusBookingById((int)booking.Status)
        //             }
        //         };
        //         return Ok(bookingDto);
        //     }
        //     Dictionary<string, string> error = new Dictionary<string, string>();
        //     error.Add("error", "Hủy đặt xe không thành công");
        //     return BadRequest(error);
        // }

        [HttpGet("/bookings/statuses")]
        public ActionResult<List<StatusDto>> GetAllStatusBooking()
        {
            // Dictionary<int, string> statuses = new Dictionary<int, string>();
            List<StatusDto> statusBookings = new List<StatusDto>();

            statusBookings.Add(new StatusDto(){Id = ((int)enumStatus.WaitDeposit), Name = "Đang chờ đặt cọc"});
            statusBookings.Add(new StatusDto(){Id = ((int)enumStatus.WaitConfirm), Name = "Đang chờ chủ xe chấp nhận"});
            statusBookings.Add(new StatusDto(){Id = ((int)enumStatus.Deposited), Name = "Đã đặt cọc"});
            statusBookings.Add(new StatusDto(){Id = ((int)enumStatus.CanceledByRenter), Name = "Bị hủy bởi khách thuê"});
            statusBookings.Add(new StatusDto(){Id = ((int)enumStatus.CanceledByLease), Name = "Bị hủy bởi chủ xe"});
            // statusBookings.Add(new StatusDto(){Id = ((int)enumStatus.CancelBySystemDeposit), Name = "Bị hủy bởi hệ thống do thời gian chờ chấp nhận quá lâu"});
            // statusBookings.Add(new StatusDto(){Id = ((int)enumStatus.CancelBySystemWaitConfirm), Name = "Bị hủy bởi hệ thống do khách thuê không đặt cọc"});
            statusBookings.Add(new StatusDto(){Id = ((int)enumStatus.ReceivedCar), Name = "Đã nhận xe"});
            statusBookings.Add(new StatusDto(){Id = ((int)enumStatus.CompletedTrip), Name = "Đã hoàn thành chuyến đi"});

            return Ok(statusBookings);
        }

        [HttpGet("/api/booking/{id}")]
        public ActionResult<BookingViewDto> GetBookingById(int id)
        {
            var booking = _bookingService.GetBookingById(id);

            if(booking == null) return Ok(null);

            BookingViewDto bookingView = new BookingViewDto()
            {
                BookingId = booking.Id,
                CarId = booking.CarId,
                CarImage = booking.Car.CarImages == null ? null : booking.Car.CarImages[0].Path,
                CarName = booking.Car.Name,
                NumberStar = booking.Car.NumberStar,
                RentDate = booking.RentDate,
                ReturnDate = booking.ReturnDate,
                // NumberDay = ((int)(booking.ReturnDate - booking.RentDate).TotalDays),
                Location = new LocationDto()
                {
                    Id = booking.LocationId,
                    Address = booking.Location.Address
                },
                Ward = booking.Location.Ward == null ? null : new WardDto()
                {
                    Id = booking.Location.WardId.Value,
                    Name = booking.Location.Ward.Name
                },
                District = new DistrictDto()
                {
                    Id = booking.Location.Ward.DistrictID,
                    Name = booking.Location.Ward.District.Name
                },
                Rule = booking.Car.Rule,
                // Total = booking.Total,
                // Cost = booking.Car.Cost,
                Deposit = booking.Total*(decimal)0.3,
                RestFee = booking.Total - booking.Total*(decimal)0.3,
                UserAccount = new UserAccountDto()
                {
                    Id = booking.Car.UserId,
                    Fullname = booking.Car.User.Fullname,
                    Rating = String.Format("{0:0.00}", booking.Car.User.RatingLease),
                    Contact = booking.Car.User.Contact
                },
                Status = new StatusDto()
                {
                    Id = ((int)booking.Status),
                    Name = _bookingService.GetNameStatusBookingById((int)booking.Status)
                },
                Message = _bookingService.GetMessageByStatus(booking.Status),
                RoleId = 1
            };

            return Ok(bookingView);
        }

        [HttpGet("/api/reservation/{id}")]
        public ActionResult<BookingViewDto> GetReservationById(int id)
        {
            var booking = _bookingService.GetBookingById(id);

            if(booking == null) return Ok(null);

            BookingViewDto bookingView = new BookingViewDto()
            {
                BookingId = booking.Id,
                CarId = booking.CarId,
                CarImage = booking.Car.CarImages == null ? null : booking.Car.CarImages[0].Path,
                CarName = booking.Car.Name,
                NumberStar = booking.Car.NumberStar,
                RentDate = booking.RentDate,
                ReturnDate = booking.ReturnDate,
                // NumberDay = ((int)(booking.ReturnDate - booking.RentDate).TotalDays),
                Location = new LocationDto()
                {
                    Id = booking.LocationId,
                    Address = booking.Location.Address
                },
                Ward = booking.Location.Ward == null ? null : new WardDto()
                {
                    Id = booking.Location.WardId.Value,
                    Name = booking.Location.Ward.Name
                },
                District = new DistrictDto()
                {
                    Id = booking.Location.Ward.DistrictID,
                    Name = booking.Location.Ward.District.Name
                },
                Rule = booking.Car.Rule,
                // Total = booking.Total,
                // Cost = booking.Car.Cost,
                Deposit = booking.Total*(decimal)0.3,
                RestFee = booking.Total - booking.Total*(decimal)0.3,
                UserAccount = new UserAccountDto()
                {
                    Id = booking.UserId,
                    Fullname = booking.User.Fullname,
                    Rating = String.Format("{0:0.00}", booking.User.RatingRent),
                    Contact = booking.User.Contact
                },
                Status = new StatusDto()
                {
                    Id = ((int)booking.Status),
                    Name = _bookingService.GetNameStatusBookingById((int)booking.Status)
                },
                Message = _bookingService.GetMessageByStatus(booking.Status),
                RoleId = 2
            };

            return Ok(bookingView);
        }

        
        [HttpGet("/api/booking/role/{idBooking}/{idUser}")]
        public ActionResult<Dictionary<string, int>> GetRoleUserInBooking(int idBooking, int idUser)
        {
            var role = _bookingService.GetRoleUserInBooking(idBooking, idUser);
            if(role == -1)
            {
                MessageReturn error = new MessageReturn()
                {
                    StatusCode = enumMessage.Fail,
                    Message = "Người dùng này không phải là người đặt xe hay chủ xe"
                };
                return Ok(error);
            }
            Dictionary<string, int> roleReturn = new Dictionary<string, int>();
            roleReturn.Add("role", role);
            return(roleReturn);
        }

        [HttpGet("/api/booking")]
        public ActionResult<List<BookingOverviewDto>> GetAllBooking()
        {
            var bookings = _bookingService.GetAllBooking();
            var bookingDtos = new List<BookingOverviewDto>();
            foreach (var booking in bookings)
            {
                bookingDtos.Add(new BookingOverviewDto()
                {
                    BookingId = booking.Id,
                    CarId = booking.CarId,
                    CarImage = booking.Car.CarImages == null ? null : booking.Car.CarImages[0].Path,
                    CarName = booking.Car.Name,
                    RentDate = booking.RentDate,
                    ReturnDate = booking.ReturnDate,
                    Total = booking.Total,
                    Status = new StatusDto()
                    {
                        Id = ((int)booking.Status),
                        Name = _bookingService.GetNameStatusBookingById((int)booking.Status)
                    }
                });
            }
            return Ok(bookingDtos);
        }

        // Admin get all booking
        [HttpGet("/api/admin/bookings")]
        public ActionResult<BookingViewAdminDto> GetAllBookingAdmin()
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
                        Status = new StatusDto()
                        {
                            Id = ((int)booking.Status),
                            Name = _bookingService.GetNameStatusBookingById((int)booking.Status)
                        },
                        CreatedAt = booking.CreatedAt,
                        Renter = new UserBooking()
                        {
                            Username = booking.User.Username,
                            Fullname = booking.User.Fullname
                        },
                        Lease = new UserBooking()
                        {
                            Username = booking.Car.User.Username,
                            Fullname = booking.Car.User.Fullname
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
    
        // Admin booking by status id
        [HttpGet("/api/admin/bookings/{idStatus}")]
        public ActionResult<BookingViewAdminDto> GetBookingsByStatus(int idStatus)
        {
            var bookings = _bookingService.GetBookingsByStatus(idStatus);
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
                        Status = new StatusDto()
                        {
                            Id = ((int)booking.Status),
                            Name = _bookingService.GetNameStatusBookingById((int)booking.Status)
                        },
                        CreatedAt = booking.CreatedAt,
                        Renter = new UserBooking()
                        {
                            Username = booking.User.Username,
                            Fullname = booking.User.Fullname
                        },
                        Lease = new UserBooking()
                        {
                            Username = booking.Car.User.Username,
                            Fullname = booking.Car.User.Fullname
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

        // Admin get booking of user
        [HttpGet("/api/admin/bookings/user/{username}")]
        public ActionResult<BookingViewAdminDto> GetBookingOfUser(string username)
        {
            var user = _userService.GetUserByUsername(username);
            if(user == null) return NotFound("User not found");

            var bookings = _bookingService.GetBookedTrip(user.Id);
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
                        Status = new StatusDto()
                        {
                            Id = ((int)booking.Status),
                            Name = _bookingService.GetNameStatusBookingById((int)booking.Status)
                        },
                        CreatedAt = booking.CreatedAt,
                        Renter = new UserBooking()
                        {
                            Username = booking.User.Username,
                            Fullname = booking.User.Fullname
                        },
                        Lease = new UserBooking()
                        {
                            Username = booking.Car.User.Username,
                            Fullname = booking.Car.User.Fullname
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
    
        // Lấy lịch sử đặt xe (những chuyến đã hoàn thành hoặc bị hủy)
        [HttpGet("/api/mybookings/history")]
        public ActionResult<BookingOverviewDto> GetHistoryBooking()
        {
            try
            {
                var username = this.User.FindFirst(ClaimTypes.NameIdentifier);
                if(username == null) return Unauthorized("Please login");
                var user = _userService.GetUserByUsername(username.Value);
                if(user == null) return Unauthorized("Please login");

                var bookings = _bookingService.GetHistoryBookings(user.Id);
                var bookingDtos = new List<BookingOverviewDto>();
                foreach (var booking in bookings)
                {
                    bookingDtos.Add(new BookingOverviewDto()
                    {
                        BookingId = booking.Id,
                        CarId = booking.CarId,
                        CarImage = booking.Car.CarImages == null ? null : booking.Car.CarImages[0].Path,
                        CarName = booking.Car.Name,
                        RentDate = booking.RentDate,
                        ReturnDate = booking.ReturnDate,
                        Total = booking.Total,
                        Status = new StatusDto()
                        {
                            Id = ((int)booking.Status),
                            Name = _bookingService.GetNameStatusBookingById((int)booking.Status)
                        },
                        RoleId = 2
                    });
                }
                return Ok(bookingDtos);
            }
            catch (System.Exception ex)
            {
                MessageReturn exception = new MessageReturn()
                    {
                        StatusCode = enumMessage.Fail,
                        Message = ex.Message
                    };
                return Ok(exception);
            }
        }

        // Lấy ra những chuyến hiện tại (chờ chấp nhận, chờ đặt cọc, đã đặt cọc, đã nhận xe)
        [HttpGet("/api/mybookings/current")]
        public ActionResult<BookingOverviewDto> GetCurrentBooking()
        {
            try
            {
                var username = this.User.FindFirst(ClaimTypes.NameIdentifier);
                if(username == null) return Unauthorized("Please login");
                var user = _userService.GetUserByUsername(username.Value);
                if(user == null) return Unauthorized("Please login");

                var bookings = _bookingService.GetCurrentBookings(user.Id);
                var bookingDtos = new List<BookingOverviewDto>();
                foreach (var booking in bookings)
                {
                    bookingDtos.Add(new BookingOverviewDto()
                    {
                        BookingId = booking.Id,
                        CarId = booking.CarId,
                        CarImage = booking.Car.CarImages == null ? null : booking.Car.CarImages[0].Path,
                        CarName = booking.Car.Name,
                        RentDate = booking.RentDate,
                        ReturnDate = booking.ReturnDate,
                        Total = booking.Total,
                        Status = new StatusDto()
                        {
                            Id = ((int)booking.Status),
                            Name = _bookingService.GetNameStatusBookingById((int)booking.Status)
                        },
                        RoleId = 2
                    });
                }
                return Ok(bookingDtos);
            }
            catch (System.Exception ex)
            {
                MessageReturn exception = new MessageReturn()
                    {
                        StatusCode = enumMessage.Fail,
                        Message = ex.Message
                    };
                return Ok(exception);
            }
        }
    
        // Lấy lịch sử đặt xe (những chuyến đã hoàn thành hoặc bị hủy)
        [HttpGet("/api/myreservations/history")]
        public ActionResult<BookingOverviewDto> GetHistoryReservations()
        {
            try
            {
                var username = this.User.FindFirst(ClaimTypes.NameIdentifier);
                if(username == null) return Unauthorized("Please login");
                var user = _userService.GetUserByUsername(username.Value);
                if(user == null) return Unauthorized("Please login");

                var bookings = _bookingService.GetHistoryReservations(user.Id);
                var bookingDtos = new List<BookingOverviewDto>();
                foreach (var booking in bookings)
                {
                    bookingDtos.Add(new BookingOverviewDto()
                    {
                        BookingId = booking.Id,
                        CarId = booking.CarId,
                        CarImage = booking.Car.CarImages == null ? null : booking.Car.CarImages[0].Path,
                        CarName = booking.Car.Name,
                        RentDate = booking.RentDate,
                        ReturnDate = booking.ReturnDate,
                        Total = booking.Total,
                        Status = new StatusDto()
                        {
                            Id = ((int)booking.Status),
                            Name = _bookingService.GetNameStatusBookingById((int)booking.Status)
                        },
                        RoleId = 1
                    });
                }
                return Ok(bookingDtos);
            }
            catch (System.Exception ex)
            {
                MessageReturn exception = new MessageReturn()
                    {
                        StatusCode = enumMessage.Fail,
                        Message = ex.Message
                    };
                return Ok(exception);
            }
        }

        // Lấy ra những chuyến hiện tại (chờ chấp nhận, chờ đặt cọc, đã đặt cọc, đã nhận xe)
        [HttpGet("/api/myreservations/current")]
        public ActionResult<BookingOverviewDto> GetCurrentReservations()
        {
            try
            {
                var username = this.User.FindFirst(ClaimTypes.NameIdentifier);
                if(username == null) return Unauthorized("Please login");
                var user = _userService.GetUserByUsername(username.Value);
                if(user == null) return Unauthorized("Please login");

                var bookings = _bookingService.GetCurrentReservations(user.Id);
                var bookingDtos = new List<BookingOverviewDto>();
                foreach (var booking in bookings)
                {
                    bookingDtos.Add(new BookingOverviewDto()
                    {
                        BookingId = booking.Id,
                        CarId = booking.CarId,
                        CarImage = booking.Car.CarImages == null ? null : booking.Car.CarImages[0].Path,
                        CarName = booking.Car.Name,
                        RentDate = booking.RentDate,
                        ReturnDate = booking.ReturnDate,
                        Total = booking.Total,
                        Status = new StatusDto()
                        {
                            Id = ((int)booking.Status),
                            Name = _bookingService.GetNameStatusBookingById((int)booking.Status)
                        },
                        RoleId = 1
                    });
                }
                return Ok(bookingDtos);
            }
            catch (System.Exception ex)
            {
                MessageReturn exception = new MessageReturn()
                    {
                        StatusCode = enumMessage.Fail,
                        Message = ex.Message
                    };
                return Ok(exception);
            }
        }
    
        // Admin get all booking
        [HttpGet("/api/admin/bookingsDetail/{idBooking}")]
        public ActionResult<BookingViewAdminDto>? GetBookingDetail(int idBooking)
        {
            var booking = _bookingService.GetBookingById(idBooking);
            if(booking == null) return Ok(null);

            var priceByDate = _bookingService.CalculatePriceAverage(booking.CarId, booking.RentDate, booking.ReturnDate);

            var bookingDto = new BookingViewAdminDto()
            {
                Id = booking.Id,
                RentDate = booking.RentDate,
                ReturnDate = booking.ReturnDate,
                NumberDays = priceByDate.Day,
                Cost = priceByDate.PriceAverage,
                Total = booking.Total,
                Status = new StatusDto()
                {
                    Id = ((int)booking.Status),
                    Name = _bookingService.GetNameStatusBookingById((int)booking.Status)
                },
                CreatedAt = booking.CreatedAt,
                Renter = new UserBooking()
                {
                    Username = booking.User.Username,
                    Fullname = booking.User.Fullname,
                    Contact = booking.User.Contact,
                },
                Lease = new UserBooking()
                {
                    Username = booking.Car.User.Username,
                    Fullname = booking.Car.User.Fullname,
                    Contact = booking.Car.User.Contact
                },
                Car = new CarBooking()
                {
                    Id = booking.CarId,
                    Name = booking.Car.Name
                },
                Location = _mapper.Map<Location, LocationDto>(booking.Location),
                Ward = _mapper.Map<Ward, WardDto>(booking.Location.Ward),
                District = _mapper.Map<District, DistrictDto>(booking.Location.Ward.District)
            };
            return Ok(bookingDto);
        }

        // Admin statist revenue by month in year
        [Route("/api/admin/statist/revenue/month/{year}")]
        [HttpGet]
        public ActionResult<Dictionary<string, decimal[]>> StatistCarsByMonth(int year)
        {
            var revenue = _paymentService.StatistRevenueByMonth(year);
            decimal[] revenueArr = new decimal[12];

            for (var i = 0; i < revenueArr.Length; i++)
            {
                revenueArr[i] = 0;
            }
            for (var i = 0; i < revenue.Count ; i++)
            {
                revenueArr[revenue[i].Time - 1] = revenue[i].Total;
            }

            Dictionary<string, decimal[]> statist = new Dictionary<string, decimal[]>();
            statist.Add("revenue", revenueArr);

            return Ok(statist);
        }

        // Admin statist revenue by day in month
        [Route("/api/admin/statist/revenue/day/{month}")]
        [HttpGet]
        public ActionResult<Dictionary<string, decimal[]>> StatistCarsByDay(int month)
        {
            var revenue = _paymentService.StatistRevenueByDay(month);

            int days = DateTime.DaysInMonth(DateTime.Now.Year, month);

            decimal[] revenueArr = new decimal[days];

            for (var i = 0; i < revenueArr.Length; i++)
            {
                revenueArr[i] = 0;
            }
            for (var i = 0; i < revenue.Count ; i++)
            {
                revenueArr[revenue[i].Time - 1] = revenue[i].Total;
            }

            Dictionary<string, decimal[]> statist = new Dictionary<string, decimal[]>();
            statist.Add("revenue", revenueArr);

            return Ok(statist);
        }

    }
}