using RentalCar.Model.Models;

namespace RentalCar.Service
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}