using RentalCar.Model.Models;

namespace RentalCar.Service
{
    public interface IUserReviewService
    {
        void AddReview(UserReview userReview);

        List<UserReview>? GetReviewsOfLease(int idUser);
    }
}