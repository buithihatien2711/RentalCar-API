using RentalCar.Data.Repositories;
using RentalCar.Model.Models;

namespace RentalCar.Service
{
    public interface IUserReviewService
    {
        void AddReviewLease(UserReview userReview);

        void AddReviewRenter(UserReview userReview);

        List<UserReview>? GetReviewsOfLease(int idUser, int pageIndex);

        List<UserReview>? GetReviewsOfRenter(int idRenter, int pageIndex);
                
        // Trả về rating và số lượng review của 1 chủ xe
        ReviewOverview GetRatingAndNumberReviewOfLease(int idLease);

        // Trả về rating và số lượng review của 1 renter
        ReviewOverview GetRatingAndNumberReviewOfRenter(int idRenter);
    }
}