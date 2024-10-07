    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization;

    namespace SOSE_API.Models
    {
        public class Booking
        {

        public int Id { get; set; }

        [ForeignKey("Tour")]
        public int TourId { get; set; }
        public Tour Tour { get; set; }

        //[ForeignKey("Customer")]
        //public int CustomerId { get; set; }
        //public Customer Customer { get; set; }

        // Use string for IdentityUser foreign key (ApplicationUser)
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }

        // Navigation property
        public ApplicationUser ApplicationUser { get; set; }

        public DateTime BookingDate { get; set; }

        public Payment Payment { get; set; }



    }
    }
