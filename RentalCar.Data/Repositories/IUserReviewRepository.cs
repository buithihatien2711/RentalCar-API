using RentalCar.Model.Models;

namespace RentalCar.Data.Repositories
{
    public interface IUserReviewRepository
    {
        List<UserReview>? GetReviewsOfLease(int idUser);

        UserReview? GetReviewById(int idReview);

        void AddReview(UserReview userReview);
    }
}