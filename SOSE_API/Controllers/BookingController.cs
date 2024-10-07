using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SOSE_API.DTO;
using SOSE_API.Interface;

namespace SOSE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {

        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var bookings = _bookingService.GetAllBookings();
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var booking = _bookingService.GetBookingById(id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        [HttpGet("searchByUserId")]
        public IActionResult SearchBookings(string userId)
        {
            var bookings = _bookingService.SearchBookinssByUserID(userId);  

            if (bookings == null || !bookings.Any())
            {
                return NotFound($"No bookings found for user with ID {userId}.");
            }

            return Ok(bookings);
        }


        [HttpGet("searchByTourId")]
        public IActionResult SearchBookingsByTourId(int tourId)
        {
            var bookings = _bookingService.SearchBookinssByTourID(tourId);

            if (bookings == null || !bookings.Any())
            {
                return NotFound($"No bookings found for the tour {tourId}.");
            }

            return Ok(bookings);
        }

        [HttpPost]
        public IActionResult CreateBooking([FromBody] CreateBookingDTO booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var InsertedBooking = _bookingService.AddBooking(booking);
            return Ok(InsertedBooking);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBooking(int id, [FromBody] BookingDTO booking)
        {
            if (!ModelState.IsValid || id != booking.Id)
            {
                return BadRequest();
            }

            var updatedBooking = _bookingService.UpdateBooking(id, booking);
            return Ok(updatedBooking);
        }


        [HttpPatch]
        public IActionResult partialUpdateBooking(int id, [FromBody] JsonPatchDocument<CreateBookingDTO> patchBooking)


        {
            if (patchBooking == null || id == 0)
            {
                return BadRequest();
            }


            var patchedBooking = _bookingService.PartialUpdateBooking(id, patchBooking);


            return Ok(patchedBooking);

        }


        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            _bookingService.DeleteTour(id);
            return Ok("Deleted succssfully ");
        }
    }
}
