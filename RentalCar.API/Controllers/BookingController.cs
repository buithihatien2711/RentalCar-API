using System.Security.Claims;
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
    public class BookingController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly IUserService _userService;
        private readonly IUploadImgService _uploadImgService;
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;
        public BookingController(ICarService carService,IUserService userService,IBookingService bookingService,IMapper mapper)
        {
            _mapper = mapper;
            _carService = carService;
            _userService = userService;
            _bookingService = bookingService;
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
            return Ok(result);
        }

        [Authorize]
        [HttpPost("/api/Car/{id}/Booking")]
        public ActionResult<string> Post(int id,[FromForm] BookingAddDto booking)
        {
            try{
                var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var userId = _userService.GetUserByUsername(username).Id;
                var location = new Location{
                    Address = booking.Address,
                    WardId = booking.WardId,
                    UserId = userId
                };

                if(_carService.CreateLocation(location) == true){
                    _carService.SaveChanges();
                };

                var value = _mapper.Map<BookingAddDto,Booking>(booking);
                value.Status = enumStatus.WaitConfirm;
                value.CreatedAt = DateTime.Now;
                value.LocationId = _carService.GetLocationByAddress(booking.Address).Id;
                value.UserId = userId;
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
                    Dictionary<string, string> message = new Dictionary<string, string>();
                    message.Add("Message", "Đã gửi yêu cầu đặt xe thành công");
                    return Ok(message);
                }
                else{
                    Dictionary<string, string> message = new Dictionary<string, string>();
                    message.Add("Message", "Gửi yêu cầu đặt xe thất bại");
                    return BadRequest(message);
                }
            }
            catch(Exception ex){
                Dictionary<string, string> message = new Dictionary<string, string>();
                message.Add("Message", ex.Message);
                return BadRequest(message);
            }
        }

        [Authorize]
        [HttpPut("/api/booking/confirm/{idBooking}")]
        public ActionResult ConfirmBooking(int idBooking)
        {
            _bookingService.ConfirmBooking(idBooking);
            if(_bookingService.SaveChanges())
            {
                var booking = _bookingService.GetBookingById(idBooking);
                var bookingDto = new BookingOverviewDto()
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
                };
                return Ok(bookingDto);
            }
            
            Dictionary<string, string> error = new Dictionary<string, string>();
            error.Add("error", "Chấp nhận yêu cầu đặt xe không thành công");
            return BadRequest(error);
        }

        [Authorize]
        [HttpPut("/api/booking/cancelBySystemWaitConfirm/{idBooking}")]
        public ActionResult CancelBookingBySystemWaitConfirm(int idBooking)
        {
            _bookingService.CancelBySystemWaitConfirm(idBooking);

            if(_bookingService.SaveChanges())
            {
                var booking = _bookingService.GetBookingById(idBooking);
                var bookingDto = new BookingOverviewDto()
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
                };
                return Ok(bookingDto);
            }
            Dictionary<string, string> error = new Dictionary<string, string>();
            error.Add("error", "Hủy đặt xe không thành công");
            return BadRequest(error);
        }
        
        [Authorize]
        [HttpPut("/api/booking/cancelBySystemWaitDeposit/{idBooking}")]
        public ActionResult CancelBookingBySystemWaitDeposit(int idBooking)
        {
            _bookingService.CancelBySystemWaitDeposit(idBooking);

            if(_bookingService.SaveChanges())
            {
                var booking = _bookingService.GetBookingById(idBooking);
                var bookingDto = new BookingOverviewDto()
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
                };
                return Ok(bookingDto);
            }
            Dictionary<string, string> error = new Dictionary<string, string>();
            error.Add("error", "Hủy đặt xe không thành công");
            return BadRequest(error);
        }

        [Authorize]
        [HttpPut("/api/booking/cancelBookingByLease/{idBooking}")]
        public ActionResult CancelBookingByLease(int idBooking)
        {
            _bookingService.CancelByLease(idBooking);

            if(_bookingService.SaveChanges())
            {
                var booking = _bookingService.GetBookingById(idBooking);
                var bookingDto = new BookingOverviewDto()
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
                };
                return Ok(bookingDto);
            }
            Dictionary<string, string> error = new Dictionary<string, string>();
            error.Add("error", "Hủy đặt xe không thành công");
            return BadRequest(error);
        }

        [Authorize]
        [HttpPut("/api/booking/cancelBookingByRenter/{idBooking}")]
        public ActionResult CancelBookingByRenter(int idBooking)
        {
            _bookingService.CancelByRenter(idBooking);

            if(_bookingService.SaveChanges())
            {
                var booking = _bookingService.GetBookingById(idBooking);
                var bookingDto = new BookingOverviewDto()
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
                };
                return Ok(bookingDto);
            }
            Dictionary<string, string> error = new Dictionary<string, string>();
            error.Add("error", "Hủy đặt xe không thành công");
            return BadRequest(error);
        }

        [HttpGet("/bookings/statuses")]
        public ActionResult<List<StatusBookingDto>> GetAllStatusBooking()
        {
            // Dictionary<int, string> statuses = new Dictionary<int, string>();
            List<StatusBookingDto> statusBookings = new List<StatusBookingDto>();

            statusBookings.Add(new StatusBookingDto(){Id = ((int)enumStatus.WaitDeposit), Name = "Đang chờ đặt cọc"});
            statusBookings.Add(new StatusBookingDto(){Id = ((int)enumStatus.WaitConfirm), Name = "Đang chờ chủ xe chấp nhận"});
            statusBookings.Add(new StatusBookingDto(){Id = ((int)enumStatus.Deposited), Name = "Đã đặt cọc"});
            statusBookings.Add(new StatusBookingDto(){Id = ((int)enumStatus.CanceledByRenter), Name = "Bị hủy bởi khách thuê"});
            statusBookings.Add(new StatusBookingDto(){Id = ((int)enumStatus.CanceledByLease), Name = "Bị hủy bởi chủ xe"});
            statusBookings.Add(new StatusBookingDto(){Id = ((int)enumStatus.Completed), Name = "Hoàn thành"});
            statusBookings.Add(new StatusBookingDto(){Id = ((int)enumStatus.CancelBySystemDeposit), Name = "Bị hủy bởi hệ thống do thời gian chờ chấp nhận quá lâu"});
            statusBookings.Add(new StatusBookingDto(){Id = ((int)enumStatus.CancelBySystemWaitConfirm), Name = "Bị hủy bởi hệ thống do khách thuê không đặt cọc"});

            return Ok(statusBookings);
        }

        [HttpGet("/api/booking/{id}")]
        public ActionResult<BookingViewDto> GetBookingById(int id)
        {
            var booking = _bookingService.GetBookingById(id);

            BookingViewDto bookingView = new BookingViewDto()
            {
                BookingId = booking.Id,
                CarId = booking.CarId,
                CarImage = booking.Car.CarImages == null ? null : booking.Car.CarImages[0].Path,
                CarName = booking.Car.Name,
                NumberStar = booking.Car.NumberStar,
                RentDate = booking.RentDate,
                ReturnDate = booking.ReturnDate,
                Location = new LocationDto()
                {
                    Id = booking.LocationId,
                    Address = booking.Location.Address
                },
                Ward = new WardDto()
                {
                    Id = booking.Location.WardId,
                    Name = booking.Location.Ward.Name
                },
                District = new DistrictDto()
                {
                    Id = booking.Location.Ward.DistrictID,
                    Name = booking.Location.Ward.District.Name
                },
                Rule = booking.Car.Rule,
                Deposit = booking.Total*(decimal)0.3,
                RestFee = booking.Total - booking.Total*(decimal)0.3,
                LeaseAccount = new LeaseAccountDto()
                {
                    Id = booking.Car.UserId,
                    Fullname = booking.Car.User.Fullname,
                    Rating = booking.Car.User.RatingLease,
                    Contact = booking.Car.User.Contact
                },
                Status = new StatusDto()
                {
                    Id = ((int)booking.Status),
                    Name = _bookingService.GetNameStatusBookingById((int)booking.Status)
                },
                Message = _bookingService.GetMeasageByStatus(booking.Status)
            };

            return Ok(bookingView);
        }

        [HttpGet("/api/booking")]
        public ActionResult<List<BookingOverviewDto>> GetAllCar()
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
    
        [HttpGet("/api/booking/mybooking")]
        public ActionResult<List<BookingOverviewDto>> GetBookedTrip()
        {
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier);
            if(username == null) return Unauthorized("Please login");
            var user = _userService.GetUserByUsername(username.Value);
            if(user == null) return Unauthorized("Please login");

            var bookings = _bookingService.GetBookedTrip(user.Id);
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
            
        [HttpGet("/api/booking/myreservation")]
        public ActionResult<List<BookingOverviewDto>> GetReservation()
        {
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier);
            if(username == null) return Unauthorized("Please login");
            var user = _userService.GetUserByUsername(username.Value);
            if(user == null) return Unauthorized("Please login");

            var bookings = _bookingService.GetReservations(user.Id);
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
         
    }
}