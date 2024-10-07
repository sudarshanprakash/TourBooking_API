namespace SOSE_API.DTO
{
    public class CreateBookingDTO
    {
        public int Id { get; set; }

        public int TourId { get; set; }

        public String UserId { get; set; }

        public DateTime BookingDate { get; set; }
        public PaymentDTO Payment { get; set; }
    }
}
