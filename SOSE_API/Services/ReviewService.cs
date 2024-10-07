using Microsoft.AspNetCore.JsonPatch;
using SOSE_API.DTO;
using SOSE_API.Interface;
using SOSE_API.Models;

namespace SOSE_API.Services
{
    public class ReviewService:IReviewService
    {


        private readonly IRepository<Review> _reviewRepository;

        public ReviewService(IRepository<Review> reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public IEnumerable<ReviewDTO> GetAllReviews()
        {
            return _reviewRepository.GetAll().Select(c => new ReviewDTO
            {
                Id = c.Id,
                Comment = c.Comment,
                Rating = c.Rating,
                TourId = c.TourId,
                UserId=c.ApplicationUserId

                // Add other properties from CustomerDTO as needed
            }); ;
        }

        public ReviewDTO GetReviewById(int id)
        {
            var review = _reviewRepository.GetById(id);
            if (review == null) return null;

            return new ReviewDTO
            {
                Id = review.Id,
                Comment = review.Comment,
                Rating= review.Rating,
                TourId= review.TourId,
                UserId= review.ApplicationUserId
                
            };
        }
        public IEnumerable<ReviewDTO> SearchReviewsByTourID(int tourId)
        {
            var reviews = _reviewRepository.Find(o => o.TourId == tourId);

            if (!reviews.Any())
            {
                return null; // Or return an empty list if preferred
            }

            return reviews.Select(review => new ReviewDTO
            {
                Id = review.Id,
                Comment = review.Comment,
                Rating = review.Rating,
                TourId = review.TourId,
                UserId = review.ApplicationUserId
            }).ToList();
        }

        public ReviewDTO AddReview(ReviewDTO reviewDto)
        {
            var review = new Review
            {
                ReviewDate = DateTime.Now,
                Rating = reviewDto.Rating,
                Comment = reviewDto.Comment,
                ApplicationUserId = reviewDto.UserId,
                TourId= reviewDto.TourId,   
                

            };

            _reviewRepository.Insert(review);
            _reviewRepository.Save();
            return new ReviewDTO
            {
                Id = review.Id,
                Comment = review.Comment,
                Rating = review.Rating,
                TourId = review.TourId,
                UserId = review.ApplicationUserId

            };
        }

       
        public void DeleteReview(int id)
        {
            _reviewRepository.Delete(id);
            _reviewRepository.Save();
        }
    }
}
