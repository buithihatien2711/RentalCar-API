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
    public class HomepageController : ControllerBase
    {
        private readonly ICarService _CarService;
        private readonly IMapper _mapper;
        public HomepageController(ICarService CarService, IMapper mapper)
        {
            _mapper = mapper;
            _CarService = CarService;
        }
        [HttpGet]
        public ActionResult<List<CarView>> Homepage()
        {
            List<Car> ListCar = _CarService.GetCars();
            List<CarView> ListCarView = new List<CarView>();

            foreach(var car in ListCar){
                ListCarView.Add(new CarView{
                    ImageAvt = _CarService.GetImageAvtByCarId(car.Id),
                    Name = car.Name,
                    Cost = car.Cost,
                    numberStar = 5.0M
                });
            }
            return ListCarView;
            // ImageAvt = _CarService.GetImageAvtByCarId()
        }
    }
}