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

        public CarReviewsController(ICarReviewService carReviewService, IMapper mapper)
        {
            _carReviewService = carReviewService;
            _mapper = mapper;
        }

        [HttpGet("{id}/{pageIndex}")]
        public ActionResult<List<CarReviewDto>> GetReviewByCar(int id, int pageIndex)
        {
            var reviews = _carReviewService.GetReviewByCar(id, pageIndex);
            return Ok(_mapper.Map<List<CarReview>, List<CarReviewDto>>(reviews));
        }
    }
}