using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalCar.API.Models;
using RentalCar.Service;
using RentalCar.Service.Models;

namespace RentalCar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [Authorize]
        // Create url payment
        [HttpPost("/{idBooking}")]
        public async Task<ActionResult> Post(int idBooking)
        {   
            var url = await _paymentService.DepositBooking(idBooking, HttpContext);
            if(String.IsNullOrEmpty(url))
            {
                Dictionary<string, string> error = new Dictionary<string, string>();
                error.Add("error", "Deposit booking fail");
                return Ok(error);
            }
            Dictionary<string, string> message = new Dictionary<string, string>();
            message.Add("url", url);
            return Ok(message);
        }
    }
}