using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.Service.Models
{
    public class PaymentInformationModel
    {
        public string BookingInfor { get; set; }
        
        public double Amount { get; set; }

        public DateTime RentDate { get; set; }

        public DateTime ReturnDate { get; set; }
    }
}