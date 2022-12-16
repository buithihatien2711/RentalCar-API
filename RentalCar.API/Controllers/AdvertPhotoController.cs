using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
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
    public class AdvertPhotoController : ControllerBase
    {
        private readonly IAdvertPhotoService _advertPhotoService;
        private readonly IUploadImgService _uploadImgService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public AdvertPhotoController(IAdvertPhotoService advertPhotoService, IUploadImgService uploadImgService,IUserService userService,IMapper mapper)
        {
            _advertPhotoService = advertPhotoService;
            _uploadImgService = uploadImgService;
            _userService = userService;
            _mapper = mapper;
        }

        //[Authorize(Roles="admin")]
        [HttpGet("")]
        public ActionResult<string> Get()
        {
            return Ok(_mapper.Map<List<AdvertisingPhoto>,List<CarImageDtos>>(_advertPhotoService.GetAllAdvert()));
        }

        [Authorize(Roles="admin")]
        [HttpPost]
        public async Task<ActionResult<string>> PostAsync(IFormFile image)
        {
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _userService.GetUserByUsername(username);
            var linkImage = await _uploadImgService.UploadImage("AdvertPhoto",username,image);
            _advertPhotoService.CreateAdvert(new AdvertisingPhoto{
                Path = linkImage,
                UserId = user.Id
            });
            if(_advertPhotoService.SaveChanges()){
                return NoContent();
            }
            else return BadRequest();
        }

        // [Authorize(Roles="admin")]
        // [HttpPut("{id}")]
        // public async Task<ActionResult<string>> PutAsync(int id,IFormFile image)
        // {
        //     var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //     var user = _userService.GetUserByUsername(username);
        //     var linkImage = await _uploadImgService.UploadImage("AdvertPhoto",username,image);
        //     _advertPhotoService.UpdateAdvert(id,new AdvertisingPhoto{
        //         Path = linkImage,
        //         UserId = user.Id
        //     });
        //     return Ok(_advertPhotoService.AdvertisingPhotoById(id));
        // }
        
        [Authorize(Roles="admin")]
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            _advertPhotoService.DeleteAdvert(id);
            if(_advertPhotoService.SaveChanges()) return NoContent();
            return BadRequest();
        }

    }
}