using System.ComponentModel.DataAnnotations;

namespace SOSE_API.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }

        public string Email { get; set; }

        public ICollection<Booking> Booking { get; set; }
        public ICollection<Review> Review { get; set; }

    }
}
