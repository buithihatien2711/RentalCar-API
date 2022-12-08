using System.Security.Claims;
using AutoMapper;
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
        public ActionResult<string> Get(int id, DateTime rentDate, DateTime returnDate)
        {
            var car = _carService.GetCarById(id);
            string message = "Thời gian đặt xe hợp lệ";
            decimal price = 0;
            int count = 0;
            for(var day = rentDate ; day <= returnDate ; day = day.AddDays(1)){
                count++;
                if(_carService.CheckScheduleByDate(id,day) == true) message = "Xe bận trong khoảng thời gian trên. Vui lòng đặt xe khác hoặc thay đổi lịch trình thích hợp.";
                var resultBefore = price;
                foreach(var priceDate in car.PriceByDates){
                    if(priceDate.Date.Date == day.Date) price += priceDate.Cost;
                }
                price = (price != resultBefore) ? price : resultBefore + car.Cost;
            }
            var result = new BookingPrice{
                PriceAverage = price/count,
                Total = price,
                Schedule =message
            };
            return Ok(result);
        }

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
                value.Status = false;
                value.CreatedAt = DateTime.Now;
                value.LocationId = _carService.GetLocationByAddress(booking.Address).Id;
                value.UserId = userId;
                value.CarId = id;

                _bookingService.CreateBooking(value);
                if(_bookingService.SaveChanges()) {
                    return Ok("Success");
                }
                else{
                    return Ok("Fail");
                }
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
    }
}