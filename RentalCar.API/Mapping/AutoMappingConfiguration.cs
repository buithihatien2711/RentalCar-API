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
            CreateMap<Car, CarViewDto>();
            CreateMap<Status, StatusDto>();
            CreateMap<Location, LocationDto>();
            CreateMap<CarModel,CarModelDto>();
            CreateMap<CarImage,CarImageDtos>();
            CreateMap<AdvertisingPhoto,CarImageDtos>();
            CreateMap<CarImgRegister,CarImageDtos>();
            CreateMap<Transmission, TransmissionDto>();
            CreateMap<FuelType, FuelTypeDto>();
            CreateMap<Location, LocationDto>();
            CreateMap<Ward, WardDto>();
            CreateMap<District, DistrictDto>();
            CreateMap<CarTypeRegister, CarTypeRegisterDto>();
            CreateMap<UserProfile, User>();
            CreateMap<License, LicenseViewDto>();
            CreateMap<LicenseViewDto, License>();
            CreateMap<User, AdminProfileDto>();
            CreateMap<PriceByDate, PriceByDateDto>();
            CreateMap<BookingAddDto,Booking>();
            CreateMap<CarSchedule, CarScheduleDto>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<User, UpdateUserDto>();
            CreateMap<User, UserProfile>()
                .ForMember(
                    dest => dest.Number,
                    opt => opt.MapFrom(src => src.License == null ? null : src.License.Number)
                );
            CreateMap<Car, CarDetailDto>()
                .ForMember(
                    dest => dest.CarModel,
                    opt => opt.MapFrom(src => src.CarModel == null ? null : src.CarModel.Name)
                )
                .ForMember(
                    dest => dest.CarModel,
                    opt => opt.MapFrom(src => src.CarModel == null ? null : (src.CarModel.CarBrand == null ? null : src.CarModel.CarBrand.Name))
                )
                .ForMember(
                    dest => dest.TransmissionDto,
                    opt => opt.MapFrom(src => src.Transmission == null ? null : new TransmissionDto()
                    {
                        Id = src.TransmissionID,
                        Name = src.Transmission.Name
                    })
                )
                .ForMember(
                    dest => dest.FuelTypeDto,
                    opt => opt.MapFrom(src => src.FuelType == null ? null : new FuelTypeDto()
                    {
                        Id = src.TransmissionID,
                        Name = src.Transmission.Name
                    })
                )
                .ForMember(
                    dest => dest.LocationDto,
                    opt => opt.MapFrom(src => src.Location == null ? null : new LocationDto()
                    {
                        Id = src.LocationId,
                        Address = src.Location.Address
                    })
                )
                .ForMember(
                    dest => dest.WardDto,
                    opt => opt.MapFrom(src => src.Location == null ? null : (src.Location.Ward == null ? null : new WardDto()
                    {
                        Id = src.Location.WardId.Value,
                        Name = src.Location.Ward.Name
                    }))
                )
                .ForMember(
                    dest => dest.DistrictDto,
                    opt => opt.MapFrom(src => src.Location == null ? null : (src.Location.Ward == null ? null : (src.Location.Ward.District == null ? null : new DistrictDto()
                    {
                        Id = src.Location.Ward.DistrictID,
                        Name = src.Location.Ward.District.Name
                    })))
                );

            CreateMap<Car, MyCarOverview>()
                .ForMember(
                    dest => dest.LocationDto,
                    opt => opt.MapFrom(src => src.Location == null ? null : new LocationDto()
                    {
                        Id = src.LocationId,
                        Address = src.Location.Address
                    }))
                .ForMember(
                    dest => dest.WardDto,
                    opt => opt.MapFrom(src => src.Location == null ? null : (src.Location.Ward == null ? null : new WardDto()
                    {
                        Id = src.Location.WardId.Value,
                        Name = src.Location.Ward.Name
                    })))
                .ForMember(
                    dest => dest.DistrictDto,
                    opt => opt.MapFrom(src => src.Location == null ? null : (src.Location.Ward == null ? null : (src.Location.Ward.District == null ? null : new DistrictDto()
                    {
                        Id = src.Location.Ward.DistrictID,
                        Name = src.Location.Ward.District.Name
                    }))))
                .ForMember(
                    dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status.Name))
                .ForMember(
                    dest => dest.Image,
                    opt => opt.MapFrom(src => src.CarImages == null ? null : src.CarImages[0].Path));

            CreateMap<Car, CarFindDto>()
                .ForMember(
                    dest => dest.LocationDto,
                    opt => opt.MapFrom(src => src.Location == null ? null : new LocationDto()
                    {
                        Id = src.LocationId,
                        Address = src.Location.Address
                    }))
                .ForMember(
                    dest => dest.WardDto,
                    opt => opt.MapFrom(src => src.Location == null ? null : (src.Location.Ward == null ? null : new WardDto()
                    {
                        Id = src.Location.WardId.Value,
                        Name = src.Location.Ward.Name
                    })))
                .ForMember(
                    dest => dest.DistrictDto,
                    opt => opt.MapFrom(src => src.Location == null ? null : (src.Location.Ward == null ? null : (src.Location.Ward.District == null ? null : new DistrictDto()
                    {
                        Id = src.Location.Ward.DistrictID,
                        Name = src.Location.Ward.District.Name
                    }))))
                .ForMember(
                    dest => dest.Image,
                    opt => opt.MapFrom(src => src.CarImages == null ? null : src.CarImages[0]))
                .ForMember(
                    dest => dest.Transmission,
                    opt => opt.MapFrom(src => src.Transmission));

            CreateMap<User, AccountDto>();

            CreateMap<CarReview, CarReviewDto>()
                .ForMember(
                    dest => dest.AccountDto,
                    opt => opt.MapFrom(src => new AccountDto()
                    {
                        Id = src.User.Id,
                        ProfileImage = src.User.ProfileImage,
                        Fullname = src.User.Fullname
                    })
                );

            CreateMap<Status, CarStatusDto>();
            CreateMap<User, UserOverviewDto>();
            CreateMap<Notification, NotiDto>();


        }
    }
}