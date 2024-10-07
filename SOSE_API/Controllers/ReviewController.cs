using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SOSE_API.DTO;
using SOSE_API.Interface;
using SOSE_API.Services;
using SOSE_API.Utility;

namespace SOSE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]


        public IActionResult Get()
        {
            var customers = _reviewService.GetAllReviews();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var tour = _reviewService.GetReviewById(id);
            if (tour == null)
            {
                return NotFound();
            }
            return Ok(tour);
        }

        [HttpGet("searchByTourId")]
        public IActionResult SearchReviews(int tourId)
        {
            var bookings = _reviewService.SearchReviewsByTourID(tourId);

            if (bookings == null || !bookings.Any())
            {
                return NotFound($"No reviews  yet  for this tour : {tourId}.");
            }

            return Ok(bookings);
        }

        [HttpPost]
        [Authorize(Roles = SD.Role_Customer)]
        public IActionResult CreateReview([FromBody] ReviewDTO review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var InsertedTour = _reviewService.AddReview(review);
            return Ok(InsertedTour);
        }




        [HttpDelete("{id}")]
        [Authorize(Roles = SD.Role_Customer)]
        public IActionResult DeleteReview(int id)
        {
            _reviewService.DeleteReview(id);
            return NoContent();
        }
    }
}
