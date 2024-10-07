using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SOSE_API.DTO;
using SOSE_API.Interface;
using SOSE_API.Models;

namespace SOSE_API.Services
{
    public class BookingService:IBookingService
    {
        private readonly IRepository<Booking> _bookingRepository;

        public BookingService(IRepository<Booking> bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public IEnumerable<GetBookingDTO> GetAllBookings()
        {
            return _bookingRepository.GetAll(b => b.Payment)
       .Select(b => new GetBookingDTO
       {
           Id=b.Id,
           TourId=b.TourId,
           UserId=b.ApplicationUserId,
           BookingDate=b.BookingDate,
           Payment= new PaymentDTO
           {
               Id=b.Payment.Id,
               Amount=b.Payment.Amount,
               PaymentMethod=b.Payment.PaymentMethod,
               Status=b.Payment.Status,
               PaymentDate=b.Payment.PaymentDate
               
               
           }

       });
        }

        public GetBookingDTO GetBookingById(int id)
        {
            var booking = _bookingRepository.GetById(id, c => c.Payment);
            if (booking == null) return null;

            return new GetBookingDTO
            {
                Id = booking.Id,
                TourId = booking.TourId,
                UserId = booking.ApplicationUserId,
                BookingDate= booking.BookingDate,
                Payment = new PaymentDTO
                {
                    Id = booking.Payment.Id,
                    Amount = booking.Payment.Amount,
                    PaymentMethod = booking.Payment.PaymentMethod,
                    Status = booking.Payment.Status,
                    PaymentDate = booking.Payment.PaymentDate



                }
            };
        }
        public IEnumerable<GetBookingDTO> SearchBookinssByUserID(string userId)
        {
            var booking = _bookingRepository.Find(o => o.ApplicationUserId == userId);

            if (!booking.Any())
            {
                return null ;
            }
            return booking.Select(booking => new GetBookingDTO
            {
                Id = booking.Id,
                TourId = booking.TourId,
                UserId = booking.ApplicationUserId,
                BookingDate = booking.BookingDate,
                
            }).ToList();
        }


        public IEnumerable<GetBookingDTO> SearchBookinssByTourID(int tourId)
        {
            var booking = _bookingRepository.Find(o => o.TourId == tourId);

            if (!booking.Any())
            {
                return null;
            }
            return booking.Select(booking => new GetBookingDTO
            {
                Id = booking.Id,
                TourId = booking.TourId,
                UserId = booking.ApplicationUserId,
                BookingDate = booking.BookingDate,

            }).ToList();
        }
        public GetBookingDTO AddBooking(CreateBookingDTO bookingDto)
        {
            var booking = new Booking
            {
               // Id = bookingDto.Id,
                BookingDate = bookingDto.BookingDate,
                ApplicationUserId = bookingDto.UserId,
                TourId = bookingDto.TourId,
                Payment=new Payment {
                    //Id=bookingDto.Payment.Id,
                    PaymentMethod= bookingDto.Payment.PaymentMethod,
                    PaymentDate= bookingDto.Payment.PaymentDate,
                    Amount=bookingDto.Payment.Amount,
                    Status=bookingDto.Payment.Status,
                    
                } 
               
               

            };

            _bookingRepository.Insert(booking);
            _bookingRepository.Save();
            return new GetBookingDTO { Id = booking.Id, TourId = booking.TourId, UserId = booking.ApplicationUserId, Payment = new PaymentDTO { Id = booking.Payment.Id, Amount = booking.Payment.Amount, PaymentMethod = booking.Payment.PaymentMethod, Status = booking.Payment.Status, PaymentDate = booking.Payment.PaymentDate } };
        }

        public GetBookingDTO UpdateBooking(int id, BookingDTO bookingDto)
        {
            var booking = _bookingRepository.GetById(id);
            if (booking == null) return null;
            Booking newBooking = new Booking()
            {
               Id = bookingDto.Id,
               BookingDate = bookingDto.BookingDate,
               ApplicationUserId = bookingDto.UserId,
                TourId = bookingDto.TourId,
              
                //Payment=bookingDto.Payment, 
            };


            _bookingRepository.Update(newBooking);
            _bookingRepository.Save();
            return new GetBookingDTO { Id = newBooking.Id, TourId = newBooking.TourId, UserId = newBooking.ApplicationUserId  };
        }

        public GetBookingDTO PartialUpdateBooking(int id, JsonPatchDocument<CreateBookingDTO> patchBooking)
        {
            // Retrieve the existing customer entity
            var bookingEntity = _bookingRepository.GetById(id, c => c.Payment);
            if (bookingEntity == null)
            {
                throw new ArgumentNullException(nameof(bookingEntity), "Customer not found");
            }

            // Map entity to DTO to apply the patch
            CreateBookingDTO bookingDto = new()
            {
                Id=bookingEntity.Id,    
                BookingDate = bookingEntity.BookingDate,
                UserId = bookingEntity.ApplicationUserId,
                TourId = bookingEntity.TourId,
                Payment= new PaymentDTO
                {
                    //Id=bookingDto.Payment.Id,
                    PaymentMethod = bookingEntity.Payment.PaymentMethod,
                    PaymentDate = bookingEntity.Payment.PaymentDate,
                    Amount = bookingEntity.Payment.Amount,
                    Status = bookingEntity.Payment.Status,

                }

                // Payment=bookingEntity.Payment,


                //ID = tourEntity.ID,
                //FullName = tourEntity.FullName,
                //Email = tourEntity.Email
            };

            // Apply the patch to the DTO
            patchBooking.ApplyTo(bookingDto);

            // Map the modified DTO back to the entity
            bookingEntity.Id = bookingDto.Id;
            bookingEntity.ApplicationUserId = bookingDto.UserId;
            bookingEntity.TourId = bookingDto.TourId;
            bookingEntity.BookingDate= bookingDto.BookingDate;
            if (bookingDto.Payment != null)
            {
                if (bookingEntity.Payment == null)
                {
                    // If no payment exists, initialize a new payment (if required)
                    bookingEntity.Payment = new Payment
                    {
                        Id = bookingDto.Payment.Id,
                        PaymentMethod = bookingDto.Payment.PaymentMethod,
                        PaymentDate = bookingDto.Payment.PaymentDate,
                        Amount = bookingDto.Payment.Amount,
                        Status = bookingDto.Payment.Status
                    };
                }
                else
                {
                    // Update the existing payment entity
                    bookingEntity.Payment.PaymentMethod = bookingDto.Payment.PaymentMethod;
                    bookingEntity.Payment.PaymentDate = bookingDto.Payment.PaymentDate;
                    bookingEntity.Payment.Amount = bookingDto.Payment.Amount;
                    bookingEntity.Payment.Status = bookingDto.Payment.Status;
                }
            }
            //bookingEntity.Payment = bookingDto.Payment; 


            // Update the entity in the database
            _bookingRepository.Update(bookingEntity);
            _bookingRepository.Save();
            return new GetBookingDTO { Id = bookingEntity.Id, TourId = bookingEntity.TourId, UserId = bookingEntity.ApplicationUserId, Payment = new PaymentDTO { Id = bookingEntity.Payment.Id, Amount = bookingEntity.Payment.Amount, PaymentMethod = bookingEntity.Payment.PaymentMethod, Status = bookingEntity.Payment.Status, PaymentDate = bookingEntity.Payment.PaymentDate } };

        }

        public void DeleteTour(int id)
        {
            _bookingRepository.Delete(id);
            _bookingRepository.Save();
        }

    }
}
