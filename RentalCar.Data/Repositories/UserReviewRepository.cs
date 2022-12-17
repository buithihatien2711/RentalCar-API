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
        public void AddReviewLease(UserReview userReview)
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
            user.RatingLease = ratings.Average();
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

        public List<UserReview>? GetReviewsOfRenter(int idRenter, int pageIndex = 1)
        {
            return _context.UserReviewUsers.Include(r => r.UserReview)
                                // get UserReviewUsers by id renter
                                .Where(r => r.UserId == idRenter)
                                .Where(r => r.RoleId == ((int)EnumClass.RoleUserInComment.Renter))
                                .Select(uru => new UserReview()
                                {
                                    Id = uru.UserReviewId,
                                    Rating = uru.UserReview.Rating,
                                    // id lease = uru.UserReview.idUser
                                    LeaseId = uru.UserReview.LeaseId,
                                    RenterId = idRenter,
                                    Content = uru.UserReview.Content,
                                    CreatedAt = uru.UserReview.CreatedAt,
                                    UpdatedAt = uru.UserReview.UpdatedAt
                                }).Skip((pageIndex - 1)*PAGE_SIZE).Take(PAGE_SIZE).ToList();
        }

        public void AddReviewRenter(UserReview userReview)
        {
            _context.UserReviews.Add(userReview);
            _context.SaveChanges();
 
            _context.UserReviewUsers.Add
            (
                new UserReviewUser()
                {
                    UserId = userReview.LeaseId,
                    UserReviewId = userReview.Id,
                    RoleId = ((int)EnumClass.RoleUserInComment.Writer)
                }
            );

            _context.UserReviewUsers.Add
            (
                new UserReviewUser()
                {
                    UserId = userReview.RenterId,
                    UserReviewId = userReview.Id,
                    RoleId = ((int)EnumClass.RoleUserInComment.Renter)
                }
            );

            // 2 cái rating khác nhau. thêm vô entity 1 cái rating nữa
            var user = _context.Users.FirstOrDefault(u => u.Id == userReview.RenterId);
            var ratings = _context.UserReviews.Where(r => r.RenterId == user.Id).Select(r => r.Rating);
            user.RatingRent = ratings.Average();
            _context.SaveChanges();
        }
    public ReviewOverview GetRatingAndNumberReviewOfLease(int idLease)
        {
            var userReview = _context.UserReviewUsers.Include(r => r.UserReview)
                                // get UserReviewUsers by id renter
                                .Where(r => r.UserId == idLease)
                                .Where(r => r.RoleId == ((int)EnumClass.RoleUserInComment.Lease))
                                .Select(r => r.UserReview);

            if(!userReview.Any()) return new ReviewOverview(){Rating = 0, NumberTrip = 0};

            return new ReviewOverview
            {
                Rating = userReview.Where(r => r.LeaseId == idLease).Select(r => r.Rating).Average(),
                NumberTrip = userReview.Where(r => r.LeaseId == idLease).Count()
            };
            
        }

        public ReviewOverview GetRatingAndNumberReviewOfRenter(int idRenter)
        {
            var userReview = _context.UserReviewUsers.Include(r => r.UserReview)
                                // get UserReviewUsers by id renter
                                .Where(r => r.UserId == idRenter)
                                .Where(r => r.RoleId == ((int)EnumClass.RoleUserInComment.Renter))
                                .Select(r => r.UserReview);

            if(!userReview.Any()) return new ReviewOverview(){Rating = 0, NumberTrip = 0};  

            return new ReviewOverview
            {
                Rating = userReview.Where(r => r.RenterId == idRenter).Select(r => r.Rating).Average(),
                NumberTrip = userReview.Where(r => r.RenterId == idRenter).Count()
            };
        }
    }

    public class ReviewOverview
    {
        public double Rating { get; set; }
        
        public int NumberTrip { get; set; }
    }
}