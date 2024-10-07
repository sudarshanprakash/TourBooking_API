using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SOSE_API.Models;

namespace SOSE_API.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        //public DbSet<Customer> Customers { get; set; }
        public DbSet<Tour> Tours { get; set; }

        public DbSet<Booking>Bookings { get; set; }

        public DbSet<Payment>Payments { get; set; }

        public DbSet<Review> Reviews { get; set; }  



    }
}
