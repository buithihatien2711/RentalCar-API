using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using RentalCar.Model.Models;

namespace RentalCar.Data.Seed
{
    public class Seed
    {
        public static void SeedRole(DataContext context)
        {
            if(context.Roles.Any()) return;

            context.Roles.Add(new Role(1, "admin"));
            context.Roles.Add(new Role(2, "lease"));
            context.Roles.Add(new Role(3, "renter"));
            
            context.SaveChanges();
        }

        public static void SeedStatus(DataContext context)
        {
            if(context.Statuses.Any()) return;

            context.Statuses.Add(new Status(1, "Đang chờ duyệt"));
            context.Statuses.Add(new Status(2, "Đã bị từ chối"));
            context.Statuses.Add(new Status(3, "Đang hoạt động"));
            context.Statuses.Add(new Status(4, "Đã cho thuê"));

            context.SaveChanges();
        }

        public static void SeedLocation(DataContext context)
        {
            //if (context.Wards.Any() || context.Districts.Any()) return;
            //var district = context.Districts.Any();
            if (context.Districts.Any()) return;

            // var locationText = System.IO.File.ReadAllText("D:/DUT/PBL6_Final/RentalCar-API/RentalCar.Data/Seed/Location.json");
            var locationText = System.IO.File.ReadAllText("D:/Ki7/PBL6-CNPM-Rentalcar/copy/RentalCar-API/RentalCar.Data/Seed/Location.json");

            var locations = JsonSerializer.Deserialize<List<LocationSeed>>(locationText);

            var group = locations.GroupBy(l => l.DistrictCode).ToList();

            foreach (var subGroup in group)
            {
                var listWard = subGroup.ToList();
                var district = new District()
                                {
                                    Id = Int32.Parse(subGroup.Key),
                                    Name = listWard[0].District
                                };
                context.Districts.Add(district);

                foreach (var ward in listWard)
                {
                    if(ward.Ward != null){
                        context.Wards.Add(
                            new Ward()
                            {
                                Name = ward.Ward,
                                DistrictID = Int32.Parse(ward.DistrictCode),
                                District = district
                            }
                        );
                    }
                }
            }
            context.SaveChanges();
        }
    
        public static void SeedModelBrand(DataContext context)
        {
            if(context.CarBrands.Any()) return;

            // var modelText = System.IO.File.ReadAllText("D:/DUT/PBL6_Final/RentalCar-API/RentalCar.Data/Seed/ModelBrand.json");
            var modelText = System.IO.File.ReadAllText("D:/Ki7/PBL6-CNPM-Rentalcar/copy/RentalCar-API/RentalCar.Data/Seed/ModelBrand.json");

            var brands = JsonSerializer.Deserialize<List<BrandSeed>>(modelText);

            foreach (var brand in brands)
            {
                context.CarBrands.Add(new CarBrand()
                {
                   Name = brand.Brand,
                });
                context.SaveChanges();

                var currentBrandId = context.CarBrands.Max(b => b.Id);
                foreach (var model in brand.Models)
                {
                    context.CarModels.Add(new CarModel()
                    {
                        Name = model,
                        CarBrandId = currentBrandId
                    });
                }
            }

            context.SaveChanges();
        }
    
        public static void SeedTransmission(DataContext context)
        {
            if(context.Transmissions.Any()) return;

            context.Transmissions.Add(new Transmission("Số tự động"));
            context.Transmissions.Add(new Transmission("Số sàn"));

            context.SaveChanges();
        }

        public static void SeedFuelType(DataContext context)
        {
            if(context.FuelTypes.Any()) return;

            context.FuelTypes.Add(new FuelType("Xăng"));
            context.FuelTypes.Add(new FuelType("Dầu diesel"));

            context.SaveChanges();
        }

        public static void SeedTypeRegister(DataContext context)
        {
            if(context.CarTypeRegisters.Any()) return;

            context.CarTypeRegisters.Add(new CarTypeRegister(1,"Cà vẹt / Giấy đăng ký xe ô tô"));
            context.CarTypeRegisters.Add(new CarTypeRegister(2,"Đăng kiểm"));
            context.CarTypeRegisters.Add(new CarTypeRegister(3,"Bảo hiểm vật chất"));
            context.SaveChanges();
        }

        public static void SeedUser(DataContext context)
        {
            if(context.Users.Any()) return;

            // var usersText = System.IO.File.ReadAllText("D:/DUT/PBL6_Final/RentalCar-API/RentalCar.Data/Seed/User.json");
            var usersText = System.IO.File.ReadAllText("D:/Ki7/PBL6-CNPM-Rentalcar/copy/RentalCar-API/RentalCar.Data/Seed/User.json");

            var users = JsonSerializer.Deserialize<List<User>>(usersText);

            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("password"));
                user.PasswordSalt = hmac.Key;
                user.CreatedAt = DateTime.Now;
                user.ProfileImage = "https://static2.yan.vn/YanNews/2167221/202102/facebook-cap-nhat-avatar-doi-voi-tai-khoan-khong-su-dung-anh-dai-dien-e4abd14d.jpg";
                context.Users.Add(user);
            }

            context.SaveChanges();
        }
    
        public static void SeedRoleUser(DataContext context)
        {
            if(context.RoleUsers.Any()) return;

            foreach (var user in context.Users)
            {
                var roleuser = new RoleUser()
                {
                    UserId = user.Id,
                    RoleId = 3,
                };
                context.Add(roleuser);
            }

            foreach (var user in context.Users.Take(50))
            {
                var roleuser = new RoleUser()
                {
                    UserId = user.Id,
                    RoleId = 2,
                };
                context.Add(roleuser);
            }
            context.SaveChanges();
        }
    }
}