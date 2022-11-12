using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalCar.API.Mapping;
using RentalCar.API.Models;
using RentalCar.Model.Models;
using RentalCar.Service;

namespace RentalCar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _CarService;
        private readonly IMapper _mapper;
        public CarController(ICarService CarService, IMapper mapper)
        {
            _mapper = mapper;
            _CarService = CarService;
        }

        [HttpGet("Homepage")]
        public ActionResult<List<CarViewDto>> Homepage()
        {
            List<Car> ListCar = _CarService.GetCars();
            List<CarViewDto> ListCarView = new List<CarViewDto>();

            foreach(var car in ListCar){
                ListCarView.Add(new CarViewDto{
                    Image = _CarService.GetImageByCarId(car.Id),
                    Name = car.Name,
                    Cost = car.Cost,
                    numberStar = car.numberStar,
                    AddressCar = car.AddressCar
                });
            }
            return Ok(ListCarView);
            // ImageAvt = _CarService.GetImageAvtByCarId()
        }
    }
}