using Microsoft.AspNetCore.Identity;

namespace SOSE_API.Models
{
    public class ApplicationUser:IdentityUser

    {
        public string FullName { get; set; }
        public int Phone {  get; set; } 
        public ICollection<Booking> Booking { get; set; }
        public ICollection<Review> Review { get; set; }
    }
}
