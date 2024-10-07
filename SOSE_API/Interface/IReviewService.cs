using Microsoft.AspNetCore.JsonPatch;
using SOSE_API.DTO;
using SOSE_API.Models;

namespace SOSE_API.Interface
{
    public interface IReviewService
    {
        IEnumerable<ReviewDTO> GetAllReviews();
        ReviewDTO GetReviewById(int id);
        ReviewDTO AddReview(ReviewDTO reviewDto);
        IEnumerable<ReviewDTO> SearchReviewsByTourID(int tourId);
        //Review UpdateTour(int id, ReviewDTO reviewDto);
        //Review PartialUpdateTour(int id, JsonPatchDocument<TourDTO> patchTour);

        void DeleteReview(int id);

    }
}
