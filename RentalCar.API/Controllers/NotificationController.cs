using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Service;
using RentalCar_API.RentalCar.Service;

namespace RentalCar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController: ControllerBase
    {
        private readonly INotificationService _notifiService;
        private readonly IUserService _userService;

        public NotificationController(INotificationService notifiService, IUserService userService)
        {
            _notifiService = notifiService;
            _userService = userService;
        }

        // [Authorize(Roles="lease, admin")]
        [HttpGet("")]
        public ActionResult<string> Get()
        {
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _userService.GetUserByUsername(username);
            var re = _notifiService.NotifiByUserId(user.Id);
            return Ok(re);

        }

        [HttpPut("{id}")]
        public ActionResult<string> Put(int id)
        {
            try{
                if(_notifiService.UpdateStatusNotifi(id)){
                    return NoContent();
                }
                else return BadRequest();
            }
            catch{
                return BadRequest();
            }

        }
    }
}