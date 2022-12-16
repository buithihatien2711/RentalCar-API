using RentalCar.Model.Models;

namespace RentalCar.Data.Repositories
{
    public class AdvertPhotoRepository : IAdvertPhotoRepository
    {
        private readonly DataContext _context;
        public AdvertPhotoRepository(DataContext context)
        {
            _context = context;
        }

        public void CreateAdvert(AdvertisingPhoto advert)
        {
            _context.AdvertisingPhotos.Add(advert);
        }

        public void DeleteAdvert(int id)
        {
            var advert = _context.AdvertisingPhotos.FirstOrDefault(p => p.Id ==id);
            if(advert == null) return;
            _context.AdvertisingPhotos.Remove(advert);
        }

        public List<AdvertisingPhoto> GetAllAdvert()
        {
            return _context.AdvertisingPhotos.ToList();
        }

        public void UpdateAdvert(int id,AdvertisingPhoto advert)
        {
            var advertGet = _context.AdvertisingPhotos.FirstOrDefault(p => p.Id ==id);
            if(advertGet == null) return;
            advertGet = advert;
            _context.SaveChanges();
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        public AdvertisingPhoto AdvertisingPhotoById(int id)
        {
            var advertising = _context.AdvertisingPhotos.FirstOrDefault(p => p.Id == id);
            if(advertising != null) return advertising;
            else return null;
        }
    }
}