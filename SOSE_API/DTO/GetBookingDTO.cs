using SOSE_API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOSE_API.DTO
{
    public class GetBookingDTO
    {
        public int Id { get; set; }

       
        public int TourId { get; set; }
        

        //[ForeignKey("Customer")]
        //public int CustomerId { get; set; }
        //public Customer Customer { get; set; }

       
        public string UserId { get; set; }

        // Navigation property


        public DateTime BookingDate { get; set; }

        public PaymentDTO Payment { get; set; }

    }
}
