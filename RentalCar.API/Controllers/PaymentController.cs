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
        [HttpPost("{idBooking}")]
        public async Task<ActionResult> Post(int idBooking)
        {   
            var url = await _paymentService.DepositBooking(idBooking, HttpContext);
            if(String.IsNullOrEmpty(url))
            {
                MessageReturn error = new MessageReturn()
                {
                    StatusCode = enumMessage.Fail,
                    Message = "Đặt cọc thất bại"
                };
                return Ok(error);
            }
            
            MessageReturn success = new MessageReturn()
            {
                StatusCode = enumMessage.Success,
                Message = url
            };
            return Ok(success);
        }

        [HttpGet("return")]
        public async Task<ActionResult<bool>> GetReturnMessage(
            [FromQuery] string vnp_TxnRef
            , [FromQuery] int vnp_Amount
            , [FromQuery] string vnp_BankCode
            , [FromQuery] string vnp_BankTranNo
            , [FromQuery] string vnp_CardType
            ,[FromQuery] string vnp_OrderInfo
            , [FromQuery] string vnp_PayDate
            , [FromQuery] string vnp_ResponseCode
            , [FromQuery] string vnp_TmnCode
            , [FromQuery] string vnp_TransactionNo
            , [FromQuery] string vnp_TransactionStatus
            , [FromQuery] string vnp_SecureHash)
        {
            PaymentResponseDto paymentResponseDto = new PaymentResponseDto()
            {
                vnp_TxnRef = vnp_TxnRef,
                vnp_Amount = vnp_Amount,
                vnp_BankCode = vnp_BankCode,
                vnp_BankTranNo = vnp_BankTranNo,
                vnp_CardType = vnp_CardType,
                vnp_OrderInfo = vnp_OrderInfo,
                vnp_PayDate = vnp_PayDate,
                vnp_ResponseCode = vnp_ResponseCode,
                vnp_TmnCode = vnp_TmnCode,
                vnp_TransactionNo = vnp_TransactionNo,
                vnp_TransactionStatus = vnp_TransactionStatus,
                vnp_SecureHash = vnp_SecureHash
            };
            // char[] delimiterChars = { '?', '=', '&'};
            // string[] words = url.Split(delimiterChars);
            var response = await _paymentService.PaymentExecute(paymentResponseDto);

            if(response)
            {
                return Ok("success");
            }
            return Ok("fail");
        }

    }
}