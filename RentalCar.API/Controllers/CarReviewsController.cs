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
    public class CarReviewsController : ControllerBase
    {
        private readonly ICarReviewService _carReviewService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public CarReviewsController(ICarReviewService carReviewService, IMapper mapper, IUserService userService)
        {
            _carReviewService = carReviewService;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet("/api/carreview/{id}/{pageIndex}")]
        public ActionResult<List<CarReviewDto>> GetReviewByCar(int id, int pageIndex)
        {
            var reviews = _carReviewService.GetReviewByCar(id, pageIndex);
            return Ok(_mapper.Map<List<CarReview>, List<CarReviewDto>>(reviews));
        }

        [HttpPost("/api/carreview/{idCar}")]
        public ActionResult<ReviewViewDto> AddCarReview([FromBody] ReviewAddDto reviewDto, int idCar)
        {
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier);
            if(username == null) return Unauthorized("Please login");
            var user = _userService.GetUserByUsername(username.Value);

            var review = new CarReview()
            {
                Content = reviewDto.Content,
                Rating = reviewDto.Value,
                CreatedAt = DateTime.Now,
                UserId = user.Id,
                CarId = idCar
            };
            
            _carReviewService.AddCarReview(review);
            return Ok(new ReviewViewDto()
            {
                Id = review.Id,
                Rating = review.Rating,
                Content = review.Content,
                CreatedAt = review.CreatedAt,
                UpdatedAt = review.UpdateAt,
                Account = _mapper.Map<User, AccountDto>(user)
            });
        }
    }
}
