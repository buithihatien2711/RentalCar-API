using Microsoft.EntityFrameworkCore;
using RentalCar.Model.Models;

namespace RentalCar.Data.Repositories
{
    public class UserReviewRepository : IUserReviewRepository
    {
        public static int PAGE_SIZE { get; set; } = 4;

        private readonly DataContext _context;

        public UserReviewRepository(DataContext context)
        {
            _context = context;
        }

        // Renter add review (Writer)
        public void AddReview(UserReview userReview)
        {
            _context.UserReviews.Add(userReview);
            _context.SaveChanges();
 
            _context.UserReviewUsers.Add
            (
                new UserReviewUser()
                {
                    UserId = userReview.LeaseId,
                    UserReviewId = userReview.Id,
                    RoleId = ((int)EnumClass.RoleUserInComment.Lease)
                }
            );

            _context.UserReviewUsers.Add
            (
                new UserReviewUser()
                {
                    UserId = userReview.RenterId,
                    UserReviewId = userReview.Id,
                    RoleId = ((int)EnumClass.RoleUserInComment.Writer)
                }
            );

            var user = _context.Users.FirstOrDefault(u => u.Id == userReview.LeaseId);
            var ratings = _context.UserReviews.Where(r => r.LeaseId == user.Id).Select(r => r.Rating);
            user.Rating = ratings.Average();
            _context.SaveChanges();
        }


        public UserReview? GetReviewById(int idReview)
        {
            return _context.UserReviews.FirstOrDefault(ur => ur.Id == idReview);
        }

        public List<UserReview>? GetReviewsOfLease(int idUser, int page = 1)
        {
            // var reviews = _context.UserReviews.Include(ur => ur.UserReviewUsers)
            //                 .Where(r => r.LeaseId == idUser)
            //                 .Where(r => );

            return _context.UserReviewUsers.Include(r => r.UserReview)
                                // get UserReviewUsers by id lease
                                .Where(r => r.UserId == idUser)
                                .Where(r => r.RoleId == ((int)EnumClass.RoleUserInComment.Lease))
                                .Select(uru => new UserReview()
                                {
                                    Id = uru.UserReviewId,
                                    Rating = uru.UserReview.Rating,
                                    // id lease = uru.UserReview.idUser
                                    LeaseId = idUser,
                                    RenterId = uru.UserReview.RenterId,
                                    Content = uru.UserReview.Content,
                                    CreatedAt = uru.UserReview.CreatedAt,
                                    UpdatedAt = uru.UserReview.UpdatedAt
                                }).Skip((page - 1)*PAGE_SIZE).Take(PAGE_SIZE).ToList();
        }
    }
}