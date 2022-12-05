using Microsoft.AspNetCore.Http;

namespace RentalCar.Service
{
    public interface IUploadImgService
    {
        Task<string> UploadImage(string folder,string username,IFormFile model);
    }
}