using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.Model.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        public int BookingId { get; set; }

        public Booking Booking { get; set; }

        public bool? Status { get; set; }

        public int Amount { get; set; }

        public string? TranCode { get; set; }

        // public string? PaymentCode { get; set; }

        public string OrderDesc { get; set; }

        public DateTime PayDate { get; set; }
    }
}