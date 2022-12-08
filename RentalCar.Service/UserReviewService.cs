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

        public void AddReview(UserReview userReview)
        {
            _userReviewRepository.AddReview(userReview);
        }

        public List<UserReview>? GetReviewsOfLease(int idUser)
        {
            return _userReviewRepository.GetReviewsOfLease(idUser);
        }
    }
}