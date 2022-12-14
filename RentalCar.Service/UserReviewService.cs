using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentalCar.Data.Repositories;
using RentalCar.Model.Models;

namespace RentalCar.Service
{
    public class UserReviewService : IUserReviewService
    {
        private readonly IUserReviewRepository _userReviewRepository;

        public UserReviewService(IUserReviewRepository userReviewRepository)
        {
            _userReviewRepository = userReviewRepository;
        }

        public void AddReviewLease(UserReview userReview)
        {
            _userReviewRepository.AddReviewLease(userReview);
        }

        public void AddReviewRenter(UserReview userReview)
        {
            _userReviewRepository.AddReviewRenter(userReview);
        }

        public List<UserReview>? GetReviewsOfLease(int idUser, int pageIndex)
        {
            return _userReviewRepository.GetReviewsOfLease(idUser, pageIndex);
        }

        public List<UserReview>? GetReviewsOfRenter(int idRenter, int pageIndex)
        {
            return _userReviewRepository.GetReviewsOfRenter(idRenter, pageIndex);
        }

        public ReviewOverview GetRatingAndNumberReviewOfLease(int idLease)
        {
            return _userReviewRepository.GetRatingAndNumberReviewOfLease(idLease);
        }

        public ReviewOverview GetRatingAndNumberReviewOfRenter(int idRenter)
        {
            return _userReviewRepository.GetRatingAndNumberReviewOfRenter(idRenter);
        }
    }
}