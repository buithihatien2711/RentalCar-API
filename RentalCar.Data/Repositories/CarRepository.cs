using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentalCar.Model.Models;

namespace RentalCar.Data.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly DataContext _context;

        public CarRepository(DataContext context)
        {
            _context = context;
        }
        public Car GetCarByCarname(string Carname)
        {
            return _context.Cars.FirstOrDefault(u => u.Name == Carname);
        }

        public Car? GetCarById(int id)
        {
            return _context.Cars.Include(p => p.Location).ThenInclude(l => l.Ward).ThenInclude(w => w.District)
                                .Include(w => w.Transmission)
                                .Include(w => w.FuelType)
                                .Include(w => w.Status)
                                .Include(w => w.CarImages)
                                .Include(u => u.CarModel)
                                .ThenInclude(l => l.CarBrand)
                                .Include(w => w.CarRegisters).ThenInclude(l => l.CarTypeRgt)
                                .Include(w => w.CarRegisters).ThenInclude(l => l.CarImgRegisters)
                                .Include(w => w.User)
                                .Include(w => w.PriceByDates)
                                .Include(w => w.CarSchedules)
                                .FirstOrDefault(u => u.Id == id);
                                
        }

        public List<Car> GetCars()
        {
            var car = _context.Cars.Include(p => p.Location).ThenInclude(l => l.Ward).ThenInclude(w => w.District)
                        .Include(w => w.Transmission)
                        .Include(w => w.FuelType)
                        .Include(w => w.Status)
                        .Include(w => w.CarImages)
                        .Include(u => u.CarModel)
                        .ThenInclude(l => l.CarBrand)
                        .Include(w => w.CarRegisters).ThenInclude(l => l.CarTypeRgt)
                        .Include(w => w.CarRegisters).ThenInclude(l => l.CarImgRegisters)
                        .Include(w => w.User)
                        .ToList();
            return car;
            // return _context.Cars.Include(p => p.Location).Include(o => o.CarModel).Include(n => n.Transmission).Include(m => m.FuelType).ToList();
        }

        // public string GetImageAvtByCarId(int CarId)
        // {
        //     return _context.CarImages.FirstOrDefault(u => u.CarId == CarId).Path;
        // }
        public List<CarBrand> GetCarBrands()
        {
            return _context.CarBrands.ToList();
        }

        public List<District> GetDistricts()
        {
            return _context.Districts.ToList();
        }

        public List<Ward> GetWards()
        {
            return _context.Wards.ToList();
        }

        public List<CarImage> GetImageByCarId(int CarId)
        {
            // return _context.CarImages.Where(p=> p.CarId == CarId).Select(p => p.Path).ToList();
            return _context.CarImages.Where(p=> p.CarId == CarId).ToList();
        }

        public void CreateCar(Car car)
        {
            _context.Cars.Add(car);
        }

        public void InsertImage(int carid, string CarImage)
        {
            // foreach(var carimage in CarImage)
            // {
            //     _context.CarImages.Add(new CarImage{
            //         CarId=carid,
            //         Path = carimage
            //     });
            // } 

            _context.CarImages.Add(new CarImage{
                CarId=carid,
                Path = CarImage
            });   
        }

        public Car GetCarByPateNumber(string Platenumber)
        {
            return _context.Cars.FirstOrDefault(u => u.Plate_number == Platenumber);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        public List<Transmission> GetTransmissions()
        {
            return _context.Transmissions.ToList();
        }

        public List<FuelType> GetFuelTypes()
        {
            return _context.FuelTypes.ToList();

        }

        public List<Ward> GetWardsByDictrictsId(int id)
        {
            return _context.Wards.Where(p => p.DistrictID == id).ToList();
        }

        public void InsertLocation(Location location)
        {
            if(_context.Locations.FirstOrDefault(u => u.Address == location.Address) == null){
                _context.Locations.Add(new Location{
                        UserId = location.UserId,
                        WardId = location.WardId,
                        Address = location.Address
                    });
            }
        }

        public bool CreateLocation(Location location)
        {
            if(_context.Locations.FirstOrDefault(u => u.Address == location.Address) == null){
             _context.Locations.Add(location);
             return true;
            }
            else{
                return false;
            }
        }

        public Location GetLocationByAddress(string Address)
        {
            return _context.Locations.FirstOrDefault(u => u.Address == Address);
        }

        public void UpdateStatusOfCar(int carId, int StatusID)
        {
            var car = _context.Cars.FirstOrDefault(u => u.Id == carId);
            car.StatusID = StatusID;
        }

        public List<CarReview>? GetCarReviewsByCarId(int id)
        {
            return _context.CarReviews.Where(r => r.CarId == id).ToList();
        }

        public void DeleteCar(Car car)
        {
            var carId = car.Id;
            var reviews = GetCarReviewsByCarId(carId);
            if(reviews != null)
            {
                foreach (var review in reviews)
                {
                    _context.Remove(review);
                }
            }
            _context.Cars.Remove(car);
        }

        public List<Car>? GetCarsByUserAndStatus(int idUser, int idStatus)
        {
            var cars = _context.Cars.Include(p => p.Location).ThenInclude(l => l.Ward).ThenInclude(w => w.District)
                                .Include(w => w.Transmission)
                                .Include(w => w.FuelType)
                                .Include(w => w.Status)
                                .Include(w => w.CarImages)
                                .Include(u => u.CarModel)
                                .ThenInclude(l => l.CarBrand)
                                .Include(w => w.CarRegisters).ThenInclude(l => l.CarTypeRgt)
                                .Include(w => w.CarRegisters).ThenInclude(l => l.CarImgRegisters)
                                .Where(c => c.UserId == idUser);

            if(GetStatuById(idStatus) == null)
            {
                return cars.ToList();
            }    

            return cars.Where(s => s.StatusID == idStatus).ToList();
        }

        public Status? GetStatuById(int idStatus)
        {
            return _context.Statuses.FirstOrDefault(s => s.Id == idStatus);
        }
        public void UpdateCarInfor(int carid, Location location,int fuelco, string des,decimal Cost)
        {
            var car = _context.Cars.FirstOrDefault(u => u.Id == carid);
            if(car != null){
                if(CreateLocation(location) == true){
                    SaveChanges();
                }
                car.Location = GetLocationByAddress(location.Address);
                car.Description = des;  
                car.FuelConsumption = fuelco;
                car.Cost = Cost;
                SaveChanges();
            }
        }

        public Ward? GetWardById(int id)
        {
            return _context.Wards.FirstOrDefault(u => u.Id == id);
        }

        public CarImage? GetCarImagebyId(int ImgId)
        {
            return _context.CarImages.FirstOrDefault(u => u.Id == ImgId);
        }
        public void DeleteCarImagebyId(int ImgId)
        { 
            _context.CarImages.Remove(GetCarImagebyId(ImgId));
        }

        public List<Car> GetCarsByStatus(int StatusId){
        if(GetStatuById(StatusId) == null)
        {
            return GetCars();
        }
        return _context.Cars.Include(p => p.Location).ThenInclude(l => l.Ward).ThenInclude(w => w.District)
                            .Include(w => w.Transmission)
                            .Include(w => w.FuelType)
                            .Include(w => w.Status)
                            .Include(w => w.CarImages)
                            .Include(u => u.CarModel)
                            .ThenInclude(l => l.CarBrand)
                            .Include(c => c.User)
                            .Include(w => w.CarRegisters).ThenInclude(l => l.CarTypeRgt)
                            .Include(w => w.CarRegisters).ThenInclude(l => l.CarImgRegisters)
                            .Where(s => s.StatusID == StatusId).ToList();
        }

        public List<CarTypeRegister> GetCarTypeRegister()
        {
            return _context.CarTypeRegisters.Include(p => p.CarRegisters).ToList();
        }
        
        public List<CarImgRegister>? GetCarImgRegistersByCarIdAndTypeId(int CarId,int CarTypeRegisterId)
        {
            var CarRegisters = _context.CarRegisters.Include(p => p.CarImgRegisters)
                                                    .Where(p => p.CarId == CarId)
                                                    .Where(p => p.CarTypeRgtId == CarTypeRegisterId)
                                                    .ToList();
            List<CarImgRegister> CarImageRegister = new List<CarImgRegister>();
            if(CarRegisters == null) CarImageRegister = null;
            else{
                // CarImageRegister = CarRegisters.CarImgRegisters;
                foreach(var carRgt in CarRegisters){
                    foreach(var carImg in carRgt.CarImgRegisters){
                        CarImageRegister.Add(carImg);
                    }
                }
            }
            return CarImageRegister;
        }
        public void InsertImageRegister(int carid, int IdType, string CarImages)
        {
            _context.CarRegisters.Add(new CarRegister{
                CarId = carid,
                CarTypeRgtId = IdType
            });
            SaveChanges();
            var CarRegister = _context.CarRegisters.Max(o=>o.Id);
            _context.CarImgRegisters.Add(new CarImgRegister{
                CarRegisterId = CarRegister,
                Path = CarImages
            });
            SaveChanges();
        }

        public void DeleteCarImageRgtbyId(int ImgId)
        {
            var image = _context.CarImgRegisters.Include(p => p.CarRegister).FirstOrDefault(u => u.Id == ImgId);
            if(image != null){
                _context.CarImgRegisters.Remove(image);
                SaveChanges();
                _context.CarRegisters.Remove(image.CarRegister);
            }

        }
        public District? GetDistrictById(int id)
        {
            return _context.Districts.FirstOrDefault(d => d.Id == id);
        }
        
        public List<Car>? GetCarsFilterSort(SearchParam searchParam)
        {
            var cars =  _context.Cars.Include(p => p.Location).ThenInclude(l => l.Ward).ThenInclude(w => w.District)
                        .Include(w => w.Transmission)
                        .Include(w => w.Status)
                        .Include(w => w.CarImages)
                        .Include(u => u.CarModel)
                        .ThenInclude(l => l.CarBrand)
                        .Include(c => c.CarSchedules)
                        .Where(c => c.StatusID == 3 || c.StatusID == 4)
                        .AsQueryable();

            #region Filtering
            // Get by District
            if (searchParam.IdDistrict.HasValue)
            {
                if (GetDistrictById(searchParam.IdDistrict.Value) != null)
                {
                    cars = cars.Where(c => c.Location.Ward.DistrictID == searchParam.IdDistrict);
                }
            }
            
            // Get car by Ward
            if (searchParam.IdWard.HasValue)
            {
                if(GetWardById(searchParam.IdWard.Value) != null)
                {
                    cars = cars.Where(c => c.Location.WardId == searchParam.IdWard);
                }
            }
            var listCars = cars.ToList();
            if (listCars != null)
            {
                // Get car by schedule
                foreach (var car in listCars.ToList())
                {
                    if (car.CarSchedules != null)
                    {
                        var schedules = car.CarSchedules;
                        foreach (var schedule in schedules)
                        {
                            // Check if two time period overlap
                            //if (!(schedule.returnDate < startdate || schedule.rentDate > enddate))
                            if ((schedule.rentDate <= searchParam.Start && schedule.returnDate >= searchParam.Start)
                                || (schedule.rentDate <= searchParam.End && schedule.returnDate >= searchParam.End)
                                || (schedule.rentDate >= searchParam.Start && schedule.returnDate <= searchParam.End))
                            {
                                listCars.Remove(car);
                                break;
                            }
                        }
                    }
                }
            }

            if(listCars == null) return null;

            cars = listCars.AsQueryable();

            // Get car by brand
            if (searchParam.IdCarBrand.HasValue)
            {
                cars = cars.Where(c => c.CarModel.CarBrandId == searchParam.IdCarBrand.Value);
            }
            
            // Get car by transmission
            if(searchParam.IdTransmission.HasValue)
            {
                cars = cars.Where(c => c.TransmissionID == searchParam.IdTransmission.Value);
            }

            // Get car by capacity
            if(searchParam.Capacity.HasValue)
            {
                cars = cars.Where(c => c.Capacity == searchParam.Capacity.Value);
            }

            #endregion

            #region Sort
            if(!string.IsNullOrEmpty(searchParam.SortBy))
            {
                switch (searchParam.SortBy)
                {
                    case ("price_asc"):
                        cars = cars.OrderBy(c => c.Cost);
                        break;
                    case ("price_desc"):
                        cars = cars.OrderByDescending(c => c.Cost);
                        break;
                    case ("rate_desc"):
                        cars = cars.OrderByDescending(c => c.NumberStar);
                        break;
                }
            }
            #endregion

            return cars.ToList();
        }

        public List<QuantityStatistics> StatistCarsByMonth(int year)
        {
            var numberCarRegister =  _context.Cars
                        .Where(c => c.CreatedAt.Value.Year == year)
                        .GroupBy(c => c.CreatedAt.Value.Month)
                        .Select(group => new QuantityStatistics{
                            Time = group.Key,
                            Count = group.Count()
                        });

            return numberCarRegister.ToList();
        }

        public List<QuantityStatistics> StatistCarsByDay(int month)
        {
            // Chỉ thống kê các ngày của tháng trong năm hiện tại
            var numberCarRegister =  _context.Cars
                        .Where(c => c.CreatedAt.Value.Month == month && c.CreatedAt.Value.Year == DateTime.Now.Year)
                        .GroupBy(c => c.CreatedAt.Value.Day)
                        .Select(group => new QuantityStatistics{
                            Time = group.Key,
                            Count = group.Count()
                        });

            return numberCarRegister.ToList();
        }

        public List<Car> GetCarsByUser(int idUser)
        {
            return _context.Cars.Include(p => p.Location).ThenInclude(l => l.Ward).ThenInclude(w => w.District)
                                .Include(w => w.Transmission)
                                .Include(w => w.FuelType)
                                .Include(w => w.Status)
                                .Include(w => w.CarImages)
                                .Include(u => u.CarModel)
                                .ThenInclude(l => l.CarBrand)
                                .Include(w => w.CarRegisters).ThenInclude(l => l.CarTypeRgt)
                                .Include(w => w.CarRegisters).ThenInclude(l => l.CarImgRegisters)
                                .Where(c => c.UserId == idUser).ToList();
        }
    }

    public class SearchParam
    {
        public int? IdDistrict { get; set; }

        public int? IdWard { get; set; }
        
        public DateTime Start { get; set; }
        
        public DateTime End { get; set; }
        
        public int? IdCarBrand { get; set; }
        
        public int? IdTransmission { get; set; }
        
        public int? Capacity { get; set; }
        
        public string? SortBy { get; set; }
    }

    public class QuantityStatistics
    {
        public int Time { get; set; }
        
        public int Count { get; set; }
    }
}