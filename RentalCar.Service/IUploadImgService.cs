using Microsoft.AspNetCore.Http;

namespace RentalCar.Service
{
    public interface IUploadImgService
    {
        Task<string> UploadImage(IFormFile model);
    }
}