using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RentalCar.API.Models;
using RentalCar.Model.Models;
using RentalCar.Service;

namespace RentalCar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ICarService _carService;

        public AccountController(IUserService userService, IMapper mapper, ICarService carService)
        {
            _carService = carService;
            _userService = userService;
            _mapper = mapper;
        }
        
        [Route("/profile/me")]
        [HttpGet]
        public ActionResult<UserProfile> Get()
        {
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            // var username = "nguyenvana";
            var user = _userService.GetUserByUsername(username);
            if (user == null) return NotFound();

            var profile = _mapper.Map<User, UserProfile>(user);
            return Ok(profile);
        }

        [HttpPut]
        public ActionResult<UserProfile> Put(UserProfile userProfile)
        {
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            // var username = "nguyenvana";

            if(string.IsNullOrEmpty(username)) return NotFound();

            var userExist = _userService.GetUserByUsername(username);

            _mapper.Map<UserProfile, User>(userProfile, userExist);

            _userService.UpdateUser(username, userExist);

            if(_userService.SaveChanges()) return NoContent();
            return BadRequest();
        }

        [HttpPatch]
        public ActionResult<UserProfile> UpdateUserPatch(JsonPatchDocument userModel)
        {
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            // var username = "nguyenvana";

            if(string.IsNullOrEmpty(username)) return NotFound();

            _userService.UpdateUserPatch(username, userModel);

            if(_userService.SaveChanges()) return NoContent();
            return BadRequest();
        }
    }
}