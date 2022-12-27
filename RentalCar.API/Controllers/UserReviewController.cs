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
    public class UserReviewController : ControllerBase
    {
        private readonly IUserReviewService _userReviewService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserReviewController(IUserReviewService userReviewService, IUserService userService, IMapper mapper)
        {
            _userReviewService = userReviewService;
            _userService = userService;
            _mapper = mapper;
        }

        // Get review của 1 chủ xe
        [HttpGet("/api/leaseComments/{idLease}/{pageIndex}")]
        public ActionResult<ReviewDto> GetReviewByLease(int idLease, int pageIndex)
        {
            var reviews = _userReviewService.GetReviewsOfLease(idLease, pageIndex);
            if(reviews == null) return Ok(null);
            List<ReviewViewDto> userReviewViewDtos = new List<ReviewViewDto>();
            foreach (var review in reviews)
            {
                userReviewViewDtos.Add(new ReviewViewDto()
                {
                    Id = review.Id,
                    Rating = review.Rating,
                    Content = review.Content,
                    CreatedAt = review.CreatedAt,
                    UpdatedAt = review.UpdatedAt,
                    Account = _mapper.Map<User, AccountDto>(_userService.GetUserById(review.RenterId))
                });
            }

            var ratingNumberTrip = _userReviewService.GetRatingAndNumberReviewOfLease(idLease);

            var reviewDto = new ReviewDto()
            {
                Rating = ratingNumberTrip.Rating,
                NumberReview = ratingNumberTrip.NumberTrip,
                ReviewViews = userReviewViewDtos
            };

            return Ok(reviewDto);
        }

        // Bình luận về một chủ xe
        [HttpPost("/api/leaseComments/{idLease}")]
        public ActionResult<MessageReturn> AddUserReview([FromBody] ReviewAddDto reviewAddDto, int idLease)
        {
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier);
            if(username == null) return Unauthorized("Please login");

            var lease = _userService.GetUserById(idLease);
            if(lease == null) return NotFound("User not exist");
            
            var userReview = new UserReview()
            {
                Rating = reviewAddDto.Value,
                LeaseId = lease.Id,
                RenterId = _userService.GetUserByUsername(username.Value).Id,
                Content = reviewAddDto.Content,
                CreatedAt = DateTime.Now
            };
            _userReviewService.AddReviewLease(userReview);

            MessageReturn success = new MessageReturn()
            {
                StatusCode = enumMessage.Success,
                Message = "Bình luận về chủ xe thành công"
            };
            return Ok(success);

            // return Ok(new ReviewViewDto()
            // {
            //     Id = userReview.Id,
            //     Rating = userReview.Rating,
            //     Content = userReview.Content,
            //     CreatedAt = userReview.CreatedAt,
            //     UpdatedAt = userReview.UpdatedAt,
            //     Account = _mapper.Map<User, AccountDto>(lease)
            // });

            // return NoContent();
        }

        // Bình luận về một người thuê xe
        [HttpPost("/api/renterComments/{idRenter}")]
        public ActionResult<MessageReturn> AddRenterReview([FromBody] ReviewAddDto reviewAddDto, int idRenter)
        {
            // Người viết cmt đang là chủ xe
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier);
            if(username == null) return Unauthorized("Please login");
            
            var renter = _userService.GetUserById(idRenter);
            if(renter == null) return NotFound("User not exist");
            
            var userReview = new UserReview()
            {
                Rating = reviewAddDto.Value,
                // Lease là người viết cmt
                LeaseId = _userService.GetUserByUsername(username.Value).Id,
                RenterId = renter.Id,
                Content = reviewAddDto.Content,
                CreatedAt = DateTime.Now
            };
            _userReviewService.AddReviewRenter(userReview);

            MessageReturn success = new MessageReturn()
            {
                StatusCode = enumMessage.Success,
                Message = "Bình luận về người thuê thành công"
            };
            return Ok(success);

            // return Ok(new ReviewViewDto()
            // {
            //     Id = userReview.Id,
            //     Rating = userReview.Rating,
            //     Content = userReview.Content,
            //     CreatedAt = userReview.CreatedAt,
            //     UpdatedAt = userReview.UpdatedAt,
            //     Account = _mapper.Map<User, AccountDto>(renter)
            // });
            
            // return NoContent();
        }

        // Get review của 1 renter
        [HttpGet("/api/renterComments/{idRenter}/{pageIndex}")]
        public ActionResult<ReviewDto> GetReviewByRenter(int idRenter, int pageIndex)
        {
            var reviews = _userReviewService.GetReviewsOfRenter(idRenter, pageIndex);
            if(reviews == null) return Ok(null);
            List<ReviewViewDto> userReviewViewDtos = new List<ReviewViewDto>();

            foreach (var review in reviews)
            {
                userReviewViewDtos.Add(new ReviewViewDto()
                {
                    Id = review.Id,
                    Rating = review.Rating,
                    Content = review.Content,
                    CreatedAt = review.CreatedAt,
                    UpdatedAt = review.UpdatedAt,
                    Account = _mapper.Map<User, AccountDto>(_userService.GetUserById(review.LeaseId))
                });
            }

            var ratingNumberTrip = _userReviewService.GetRatingAndNumberReviewOfRenter(idRenter);

            var reviewDto = new ReviewDto()
            {
                Rating = ratingNumberTrip.Rating,
                NumberReview = ratingNumberTrip.NumberTrip,
                ReviewViews = userReviewViewDtos
            };

            return Ok(reviewDto);
        }

        // Bình luận về một user
        [HttpPost("/api/userComment/{idUser}")]
        public ActionResult<MessageReturn> AddUserReview([FromBody] ReviewAddDto reviewAddDto, int idUser, int roleId)
        {
            // Nếu role = 1 người viết là chủ xe, role = 2 người viết là người thuê
            if(roleId == 1)
            {
                // Người viết cmt đang là chủ xe
                var username = this.User.FindFirst(ClaimTypes.NameIdentifier);
                if(username == null) return Unauthorized("Please login");
                
                var renter = _userService.GetUserById(idUser);
                if(renter == null) return NotFound("User not exist");
                
                var userReview = new UserReview()
                {
                    Rating = reviewAddDto.Value,
                    // Lease là người viết cmt
                    LeaseId = _userService.GetUserByUsername(username.Value).Id,
                    RenterId = renter.Id,
                    Content = reviewAddDto.Content,
                    CreatedAt = DateTime.Now
                };
                _userReviewService.AddReviewRenter(userReview);

                MessageReturn success = new MessageReturn()
                {
                    StatusCode = enumMessage.Success,
                    Message = "Bình luận về người thuê thành công"
                };
                return Ok(success);
            }

            if(roleId == 2)
            {
                // Người viết đang là renter review về chủ xe
                var username = this.User.FindFirst(ClaimTypes.NameIdentifier);
                if(username == null) return Unauthorized("Please login");

                var lease = _userService.GetUserById(idUser);
                if(lease == null) return NotFound("User not exist");
                
                var userReview = new UserReview()
                {
                    Rating = reviewAddDto.Value,
                    LeaseId = lease.Id,
                    RenterId = _userService.GetUserByUsername(username.Value).Id,
                    Content = reviewAddDto.Content,
                    CreatedAt = DateTime.Now
                };
                _userReviewService.AddReviewLease(userReview);

                MessageReturn success = new MessageReturn()
                {
                    StatusCode = enumMessage.Success,
                    Message = "Bình luận về chủ xe thành công"
                };
                return Ok(success);
            }

            MessageReturn error = new MessageReturn()
            {
                StatusCode = enumMessage.Fail,
                Message = "Không thể bình luận về người này"
            };
            return Ok(error);
        }

    }
}