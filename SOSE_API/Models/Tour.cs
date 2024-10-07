using System.ComponentModel.DataAnnotations;

namespace SOSE_API.Models
{
    public class Tour
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<Booking> Booking { get; set; }
        public ICollection<Review>Review { get; set; }

    }
}
