using AutoMapper;
using RentalCar.API.Models;
using RentalCar.Model.Models;

namespace RentalCar.API.Mapping
{
    public class AutoMappingConfiguration : Profile
    {
        public AutoMappingConfiguration()
        {
            CreateMap<CarBrand, CarBrandDto>();
            CreateMap<CarModel,CarModelDto>();
            CreateMap<Transmission, TransmissionDto>();
            CreateMap<FuelType, FuelTypeDto>();
            CreateMap<Location, LocationDto>();
            CreateMap<Ward, WardDto>();
            CreateMap<UserProfile, User>();
            CreateMap<User, UserProfile>().ForMember(
                dest => dest.LicenseDto,
                opt => opt.MapFrom(src => src.License == null ? null : new LicenseDto()
                                    {
                                        Number = src.License.Number,
                                        Name = src.License.Name,
                                        DateOfBirth = src.License.DateOfBirth,
                                        Image = src.License.Image
                                    })
            );
        }
    }
}