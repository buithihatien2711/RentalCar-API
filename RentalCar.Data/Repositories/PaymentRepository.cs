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
    }
}