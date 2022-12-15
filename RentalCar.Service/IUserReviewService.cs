using RentalCar.Model.Models;

namespace RentalCar.Service
{
    public interface IUserReviewService
    {
        void AddReview(UserReview userReview);

        void AddReviewRenter(UserReview userReview);

        List<UserReview>? GetReviewsOfLease(int idUser, int pageIndex);

        List<UserReview>? GetReviewsOfRenter(int idRenter, int pageIndex);
    }
}