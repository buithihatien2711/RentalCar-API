using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Service;
using RentalCar.API.Models;
using Microsoft.AspNetCore.Authorization;
namespace RentalCar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class carregistermodeController : ControllerBase
    {
        private readonly ICarService _CarService;
        public carregistermodeController(ICarService CarService)
        {
            _CarService = CarService;
        }

        // [HttpGet("selfdrive")]
        // public ActionResult<string> CarRegister()
        // {
        //     var CarBrands = _CarService.GetCarBrands();
        //     var CarModels = _CarService.GetCarModels();
        //     // var District = _CarService.
        //     return (Ok(CarBrands,CarModels));
        // }


        // [Authorize(Roles = "")]
        // [HttpPost("selfdrive")]
        // public ActionResult<carRegister> Selfdrive(carRegister)
        // {

        // }
    }
}