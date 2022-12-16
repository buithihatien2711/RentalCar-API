using RentalCar.Model.Models;

namespace RentalCar.Service
{
    public interface IAdvertPhotoService
    {
        List<AdvertisingPhoto> GetAllAdvert();
        void DeleteAdvert(int id);
        void CreateAdvert(AdvertisingPhoto advert);
        void UpdateAdvert(int id,AdvertisingPhoto advert);
        AdvertisingPhoto AdvertisingPhotoById(int id);

        bool SaveChanges();
    }
}