using AutoMapper;
using RentalCar.API.Models;
using RentalCar.Model.Models;

namespace RentalCar.API.Mapping
{
    public class AutoMappingConfiguration : Profile
    {
        public AutoMappingConfiguration()
        {
            CreateMap<CarViewDto, User>();
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