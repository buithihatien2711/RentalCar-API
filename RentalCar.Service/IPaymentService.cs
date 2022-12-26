using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RentalCar.Data.Repositories;
using RentalCar.Service.Models;

namespace RentalCar.Service
{
    public interface IPaymentService
    {
        // Tạo ra url thanh toán tạo VNPAY
        //Parameter: PaymentInformationModel model này sẽ chứa các thông tin của hóa đơn thanh toán
        //       HttpContext : lấy địa chỉ IP Address của client thanh toán đơn hàng đó
        Task<string> CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
        
        // Kiểm tra thông tin giao dịch và sẽ lưu lại thông tin đó sau khi thanh toán thành công
        // Parameter: IQueryCollection : thông tin trên URL mà VnPay trả về trong các parameter 
        //           sau khi thanh toán thành công hoặc lỗi
        // Task<PaymentResponseModel> PaymentExecute(IQueryCollection collection); 
        Task<bool> PaymentExecute(PaymentResponseDto collection); 

        Task<string> DepositBooking(int idBooking, HttpContext context);

        List<TotalStatistics> StatistRevenueByMonth(int year);

        List<TotalStatistics> StatistRevenueByDay(int month);
    }
}