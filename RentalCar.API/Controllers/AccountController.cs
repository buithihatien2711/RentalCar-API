using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        
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
    }
}