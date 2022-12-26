using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentalCar.Model.Models;

namespace RentalCar.Data.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly DataContext _context;

        public PaymentRepository(DataContext context)
        {
            _context = context;
        }

        public void AddPayment(Payment payment)
        {
            _context.Add(payment);
        }

        public bool SaveChange()
        {
            return _context.SaveChanges() > 0;
        }

        public List<TotalStatistics> StatistRevenueByDay(int month)
        {
            // Chỉ thống kê các ngày của tháng trong năm hiện tại
            var revenue =  _context.Payments
                        .Where(p => p.PayDate.Month == month && p.PayDate.Year == DateTime.Now.Year)
                        .GroupBy(p => p.PayDate.Day)
                        .Select(group => new TotalStatistics{
                            Time = group.Key,
                            Total = (decimal)(group.Sum(p => p.Amount)*0.2)
                        });

            return revenue.ToList();
        }

        public List<TotalStatistics> StatistRevenueByMonth(int year)
        {
            var revenue =  _context.Payments
                        .Where(p => p.PayDate.Year == year)
                        .GroupBy(c => c.PayDate.Month)
                        .Select(group => new TotalStatistics{
                            Time = group.Key,
                            Total = (decimal)(group.Sum(p => p.Amount)*0.2)
                        });

            return revenue.ToList();
        }
    }

    public class TotalStatistics
    {
        public int Time { get; set; }
        
        public decimal Total { get; set; }
    }
}