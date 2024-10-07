namespace SOSE_API.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }  // Credit card, PayPal, etc.
        public string Status { get; set; }  // Paid, Pending, Failed

        // Foreign Key and Navigation Properties
        public int BookingId { get; set; }
        public Booking Booking { get; set; }
    }
}
