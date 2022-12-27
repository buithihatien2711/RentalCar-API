using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalCar.API.Models;
using RentalCar.Model.Models;
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
        private readonly IMapper _mapper;

        public NotificationController(INotificationService notifiService, IMapper mapper,IUserService userService)
        {
            _mapper = mapper;
            _notifiService = notifiService;
            _userService = userService;
        }

        // [Authorize(Roles="lease, admin")]
        [HttpGet("")]
        public ActionResult<string> Get()
        {
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _userService.GetUserByUsername(username);
            var re = _mapper.Map<List<Notification>,List<NotiDto>>(_notifiService.NotifiByUserId(user.Id));
            return Ok(re);

        }

        [HttpGet("NotRead")]
        public ActionResult<string> GetIsRead()
        {
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _userService.GetUserByUsername(username);
            var re = _mapper.Map<List<Notification>,List<NotiDto>>(_notifiService.NotifiNotReadByUserId(user.Id));
            return Ok(re);

        }

        [HttpPut("{id}")]
        public ActionResult<string> Put(int id)
        {
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _userService.GetUserByUsername(username);
            try{
                if(_notifiService.UpdateStatusNotifi(id)){
                    // var re = _notifiService.NotifiByUserId(user.Id);
                    MessageReturn success = new MessageReturn()
                    {
                        StatusCode = enumMessage.Fail,
                        Message = "Cập nhật thông báo thành công"
                    };
                    return Ok(success);
                }
                else {
                    MessageReturn fail = new MessageReturn()
                    {
                        StatusCode = enumMessage.Fail,
                        Message = "Lỗi server"
                    };
                    return Ok(fail);
                }
            }
            catch{
                MessageReturn fail = new MessageReturn()
                {
                    StatusCode = enumMessage.Fail,
                    Message = "Lỗi server"
                };
                return Ok(fail);
            }

        }

        // [HttpPut("{id}/IsRead")]
        // public ActionResult<string> PutIsRead(int id)
        // {
        //     var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //     var user = _userService.GetUserByUsername(username);
        //     try{
        //         if(_notifiService.UpdateStatusNotifi(id)){
        //             // var re = _notifiService.NotifiIsReadByUserId(user.Id);
        //             MessageReturn success = new MessageReturn()
        //             {
        //                 StatusCode = enumMessage.Fail,
        //                 Message = "Cập nhật thông báo thành công"
        //             };
        //             return Ok(success);
        //         }
        //         else{
        //             MessageReturn fail = new MessageReturn()
        //             {
        //                 StatusCode = enumMessage.Fail,
        //                 Message = "Lỗi server"
        //             };
        //             return Ok(fail);
        //         };
        //     }
        //     catch{
        //         MessageReturn fail = new MessageReturn()
        //         {
        //             StatusCode = enumMessage.Fail,
        //             Message = "Lỗi server"
        //         };
        //         return Ok(fail);
        //     }

        // }
    }
}