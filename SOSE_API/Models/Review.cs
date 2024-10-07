using System.ComponentModel.DataAnnotations.Schema;

namespace SOSE_API.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }

        // Foreign Key and Navigation Properties for Customer
        //public int CustomerId { get; set; }
        //public Customer Customer { get; set; }  // Correct casing for consistency

        // Foreign Key and Navigation Properties for ApplicationUser (assuming IdentityUser)
        [ForeignKey("ApplicationUser")] // Specify the foreign key
        public string ApplicationUserId { get; set; }  // Change type to string for IdentityUser

        public ApplicationUser ApplicationUser { get; set; }

        // Foreign Key and Navigation Properties for Tour
        public int TourId { get; set; }
        public Tour Tour { get; set; }
    }
}
