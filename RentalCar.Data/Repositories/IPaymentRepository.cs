using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentalCar.Model.Models;

namespace RentalCar.Data.Repositories
{
    public interface IPaymentRepository
    {
        void AddPayment(Payment payment);

        bool SaveChange();

        List<TotalStatistics> StatistRevenueByMonth(int year);

        List<TotalStatistics> StatistRevenueByDay(int month);
    }
}