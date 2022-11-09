using System.ComponentModel.DataAnnotations;

namespace RentalCar.Model.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        
        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }
        
        [MaxLength(30)]
        public string Color { get; set; }
        
        public string Capacity { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string Plate_number { get; set; }
        
        public int Cost { get; set; }
        
        public DateTime? CreatedAt { get; set; }
        
        public DateTime? UpdateAt { get; set; }
        
        public int CarModelId { get; set; }
        
        public CarModel CarModel { get; set; }
        
        public int StatusID  { get; set; }
        
        public Status Status { get; set; }

        public int UserId { get; set; }
        
        public User User { get; set; }

        //Năm sản xuất
        public int? YearManufacture { get; set; }
        //Truyền động
        public string? Transmission { get; set; }
        //Loại nhiên liệu
        public string? FuelType { get; set; }
        //Mức tiêu thụ nhiên liệu
        public int? FuelConsumption { get; set; }
        //AddressCar
        public string AddressCar { get; set; }
        List<CarImage>? CarImages { get; set; }

        
        // Thieu BusyId khoa ngoai bang Car_Busydate
    }
}