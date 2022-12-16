using RentalCar.Model.Models;

namespace RentalCar.Data.Repositories
{
    public interface IAdvertPhotoRepository
    {
        List<AdvertisingPhoto> GetAllAdvert();
        AdvertisingPhoto AdvertisingPhotoById(int id);
        void DeleteAdvert(int id);
        void CreateAdvert(AdvertisingPhoto advert);
        void UpdateAdvert(int id,AdvertisingPhoto advert);
        bool SaveChanges();
    }
}