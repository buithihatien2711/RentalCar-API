using RentalCar.Data.Repositories;
using RentalCar.Model.Models;
using RentalCar.Service;

namespace RentalCar.Service
{
    public class AdvertPhotoService : IAdvertPhotoService
    {
        private readonly IAdvertPhotoRepository _advertPhotoRepository;
        public AdvertPhotoService(IAdvertPhotoRepository advertPhotoRepository)
        {
            _advertPhotoRepository = advertPhotoRepository;
        }

        public AdvertisingPhoto AdvertisingPhotoById(int id)
        {
            return _advertPhotoRepository.AdvertisingPhotoById(id);
        }

        public void CreateAdvert(AdvertisingPhoto advert)
        {
            _advertPhotoRepository.CreateAdvert(advert);
            // SaveChanges();
        }

        public void DeleteAdvert(int id)
        {
            _advertPhotoRepository.DeleteAdvert(id);
        }

        public List<AdvertisingPhoto> GetAllAdvert()
        {
            return _advertPhotoRepository.GetAllAdvert();
        }

        public bool SaveChanges()
        {
            return _advertPhotoRepository.SaveChanges();
        }

        public void UpdateAdvert(int id, AdvertisingPhoto advert)
        {
            _advertPhotoRepository.UpdateAdvert(id,advert);
            // SaveChanges();
        }
    }
}