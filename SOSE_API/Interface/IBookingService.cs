using Microsoft.AspNetCore.JsonPatch;
using SOSE_API.DTO;
using SOSE_API.Models;

namespace SOSE_API.Interface
{
    public interface IBookingService
    {

        IEnumerable<GetBookingDTO> GetAllBookings();
        GetBookingDTO GetBookingById(int id);
        IEnumerable<GetBookingDTO> SearchBookinssByUserID(string userId);
        IEnumerable<GetBookingDTO> SearchBookinssByTourID(int userId);
        GetBookingDTO AddBooking(CreateBookingDTO bookingDto);
        GetBookingDTO UpdateBooking(int id, BookingDTO bookingDto);
        GetBookingDTO PartialUpdateBooking(int id, JsonPatchDocument<CreateBookingDTO> patchBooking);

        void DeleteTour(int id);
    }
}
