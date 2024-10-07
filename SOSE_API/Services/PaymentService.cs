using Microsoft.AspNetCore.JsonPatch;
using SOSE_API.DTO;
using SOSE_API.Interface;
using SOSE_API.Models;

namespace SOSE_API.Services
{
    public class PaymentService
    {
        private readonly IRepository<Payment> _paymentRepository;

        public PaymentService(IRepository<Payment> paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        //public IEnumerable<Payment> GetAllPayment()
        //{
        //    return _paymentRepository.GetAll();
        //}

        //public Payment GetPaymentById(int id)
        //{
        //    var payment = _paymentRepository.GetById(id);
        //    if (payment == null) return null;

        //    return payment;
        //}

        //public Payment AddPayment(PaymentDTO paymentDto)
        //{
        //    var payment = new Payment
        //    {
        //        //Id = bookingDto.Id,
        //        Amount = paymentDto.Amount,
        //        PaymentDate = paymentDto.PaymentDate,
        //        PaymentMethod = paymentDto.PaymentMethod,
        //        Status = paymentDto.Status,
        //        BookingId = paymentDto.BookingId,
                



        //    };

        //    _paymentRepository.Insert();
        //    _paymentRepository.Save();
        //    return booking;
        //}

        //public Booking UpdateBooking(int id, BookingDTO bookingDto)
        //{
        //    var tour = _bookingRepository.GetById(id);
        //    if (tour == null) return null;
        //    Booking newBooking = new Booking()
        //    {
        //        Id = bookingDto.Id,
        //        BookingDate = bookingDto.BookingDate,
        //        CustomerId = bookingDto.CustomerId,
        //        TourId = bookingDto.TourId,
        //    };


        //    _bookingRepository.Update(newBooking);
        //    _bookingRepository.Save();
        //    return newBooking;
        //}

        //public Booking PartialUpdateBooking(int id, JsonPatchDocument<BookingDTO> patchBooking)
        //{
        //    // Retrieve the existing customer entity
        //    var bookingEntity = _bookingRepository.GetById(id);
        //    if (bookingEntity == null)
        //    {
        //        throw new ArgumentNullException(nameof(bookingEntity), "Customer not found");
        //    }

        //    // Map entity to DTO to apply the patch
        //    BookingDTO bookingDto = new()
        //    {
        //        Id = bookingEntity.Id,
        //        BookingDate = bookingEntity.BookingDate,
        //        CustomerId = bookingEntity.CustomerId,
        //        TourId = bookingEntity.TourId,


        //        //ID = tourEntity.ID,
        //        //FullName = tourEntity.FullName,
        //        //Email = tourEntity.Email
        //    };

        //    // Apply the patch to the DTO
        //    patchBooking.ApplyTo(bookingDto);

        //    // Map the modified DTO back to the entity
        //    bookingEntity.Id = bookingDto.Id;
        //    bookingEntity.CustomerId = bookingDto.CustomerId;
        //    bookingEntity.TourId = bookingEntity.TourId;
        //    bookingEntity.BookingDate = bookingDto.BookingDate;



        //    // Update the entity in the database
        //    _bookingRepository.Update(bookingEntity);
        //    _bookingRepository.Save();
        //    return bookingEntity;

        //}

        //public void DeleteTour(int id)
        //{
        //    _bookingRepository.Delete(id);
        //    _bookingRepository.Save();
        //}

    }
}
