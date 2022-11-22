using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentalCar.Model.Models;

namespace RentalCar.API.Models
{
    public class CarInfoView_UpdateDto
    {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Plate_number { get; set; }
            public decimal Cost { get; set; }
            public LocationDto location { get; set; }
            public WardDto Ward { get; set; }
            public District District{ get; set; }
            public StatusDto Status{ get; set; }
            public int Capacity { get; set; }
            public TransmissionDto Transmission { get; set; }
            public FuelTypeDto FuelType { get; set; }
            public int FuelConsumption { get; set; }
            public string? Description { get; set; }

            public List<CarImageDtos> carImages { get; set; }

            // public List<WardDto> Wards { get; set; }
            // public List<District> Districts { get; set; }

    }
}