using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RentalCar.API.Models;
using RentalCar.Model.Models;
using RentalCar.Service;

namespace RentalCar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarStatusController : ControllerBase
    {
        private readonly ICarStatusService _carStatusService;
        private readonly IMapper _mapper;

        public CarStatusController(ICarStatusService carStatusService, IMapper mapper)
        {
            _mapper = mapper;
            _carStatusService = carStatusService;
            
        }

        [HttpGet]
        public ActionResult<List<CarStatusDto>> Get()
        {
            var statuses = _carStatusService.GetStatuses();
            statuses.Add(new Status()
            {
                Id = 0,
                Name = "Tất Cả"
            });
            return Ok(_mapper.Map<List<Status>, List<CarStatusDto>>(statuses));
        }
    }
}