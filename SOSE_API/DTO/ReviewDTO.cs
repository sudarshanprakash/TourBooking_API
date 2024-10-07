using SOSE_API.Models;

namespace SOSE_API.DTO
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        //public DateTime ReviewDate { get; set; }

        // Foreign Key and Navigation Properties
        public string UserId { get; set; }
        
        public int TourId { get; set; }
        
    }
}
