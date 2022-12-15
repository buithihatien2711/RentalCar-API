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

        // // Get my review
        // [HttpGet("/api/myComment/{pageIndex}")]
        // public ActionResult<List<ReviewViewDto>> GetMyReview(int pageIndex)
        // {
        //     var username = this.User.FindFirst(ClaimTypes.NameIdentifier);
        //     if(username == null) return Unauthorized("Please login");
        //     var lease = _userService.GetUserByUsername(username.Value);
        //     if(lease == null) return Unauthorized("Please login");

        //     var reviews = _userReviewService.GetReviewsOfLease(lease.Id, pageIndex);
        //     if(reviews == null) return Ok(null);
        //     List<ReviewViewDto> userReviewViewDtos = new List<ReviewViewDto>();
        //     foreach (var review in reviews)
        //     {
        //         userReviewViewDtos.Add(new ReviewViewDto()
        //         {
        //             Id = review.Id,
        //             Rating = review.Rating,
        //             Content = review.Content,
        //             CreatedAt = review.CreatedAt,
        //             UpdatedAt = review.UpdatedAt,
        //             Account = _mapper.Map<User, AccountDto>(_userService.GetUserById(review.RenterId))
        //         });
        //     }
        //     return Ok(userReviewViewDtos);
        // }

        // Get review của 1 lease
        [HttpGet("/api/leaseComments/{idLease}/{pageIndex}")]
        public ActionResult<List<ReviewViewDto>> GetReviewByLease(int idLease, int pageIndex)
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
            return Ok(userReviewViewDtos);
        }

        // Bình luận về một chủ xe
        [HttpPost("/api/leaseComments/{idLease}")]
        public ActionResult<ReviewViewDto> AddUserReview([FromBody] ReviewAddDto reviewAddDto, int idLease)
        {
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier);
            if(username == null) return Unauthorized("Please login");

            var lease = _userService.GetUserById(idLease);
            if(lease == null) return NotFound("User not exist");
            
            var userReview = new UserReview()
            {
                Rating = reviewAddDto.Rating,
                LeaseId = lease.Id,
                RenterId = _userService.GetUserByUsername(username.Value).Id,
                Content = reviewAddDto.Content,
                CreatedAt = DateTime.Now
            };
            _userReviewService.AddReview(userReview);

            return NoContent();
            // return Ok(new ReviewViewDto()
            // {
            //     Id = userReview.Id,
            //     Rating = userReview.Rating,
            //     Content = userReview.Content,
            //     CreatedAt = userReview.CreatedAt,
            //     UpdatedAt = userReview.UpdatedAt,
            //     Account = _mapper.Map<User, AccountDto>(lease)
            // });
        }

        // Bình luận về một người thuê xe
        [HttpPost("/api/renterComments/{idRenter}")]
        public ActionResult<ReviewViewDto> AddRenterReview([FromBody] ReviewAddDto reviewAddDto, int idRenter)
        {
            // Người viết cmt đang là chủ xe
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier);
            if(username == null) return Unauthorized("Please login");
            
            var renter = _userService.GetUserById(idRenter);
            if(renter == null) return NotFound("User not exist");
            
            var userReview = new UserReview()
            {
                Rating = reviewAddDto.Rating,
                // Lease là người viết cmt
                LeaseId = _userService.GetUserByUsername(username.Value).Id,
                RenterId = renter.Id,
                Content = reviewAddDto.Content,
                CreatedAt = DateTime.Now
            };
            _userReviewService.AddReviewRenter(userReview);

            // return Ok(new ReviewViewDto()
            // {
            //     Id = userReview.Id,
            //     Rating = userReview.Rating,
            //     Content = userReview.Content,
            //     CreatedAt = userReview.CreatedAt,
            //     UpdatedAt = userReview.UpdatedAt,
            //     Account = _mapper.Map<User, AccountDto>(renter)
            // });
            
            return NoContent();
        }

        // Get review của 1 renter
        [HttpGet("/api/renterComments/{idRenter}/{pageIndex}")]
        public ActionResult<List<ReviewViewDto>> GetReviewByRenter(int idRenter, int pageIndex)
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
            return Ok(userReviewViewDtos);
        }
    }
}