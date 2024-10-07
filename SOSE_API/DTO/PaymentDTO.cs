using SOSE_API.Models;
using System.Diagnostics.CodeAnalysis;

namespace SOSE_API.DTO
{
    public class PaymentDTO
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }  // Credit card, PayPal, etc.

        
        public string Status { get; set; }  // Paid, Pending, Failed

        // Foreign Key and Navigation Properties
        //public int BookingId { get; set; }

       
    }
}
