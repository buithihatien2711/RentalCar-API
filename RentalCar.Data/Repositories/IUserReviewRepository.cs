using RentalCar.Model.Models;

namespace RentalCar.Data.Repositories
{
    public interface IUserReviewRepository
    {
        List<UserReview>? GetReviewsOfLease(int idUser, int page = 1);

        UserReview? GetReviewById(int idReview);

        void AddReviewLease(UserReview userReview);

        List<UserReview>? GetReviewsOfRenter(int idRenter, int pageIndex = 1);

        void AddReviewRenter(UserReview userReview);

        // Trả về rating và số lượng review
        ReviewOverview GetRatingAndNumberReviewOfLease(int idLease);

        ReviewOverview GetRatingAndNumberReviewOfRenter(int idRenter);
    }
}