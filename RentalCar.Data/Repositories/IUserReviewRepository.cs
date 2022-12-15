using RentalCar.Model.Models;

namespace RentalCar.Data.Repositories
{
    public interface IUserReviewRepository
    {
        List<UserReview>? GetReviewsOfLease(int idUser, int page = 1);

        UserReview? GetReviewById(int idReview);

        void AddReview(UserReview userReview);

        List<UserReview>? GetReviewsOfRenter(int idRenter, int pageIndex = 1);

        void AddReviewRenter(UserReview userReview);
    }
}