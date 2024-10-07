using SOSE_API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOSE_API.DTO
{
    public class BookingDTO
    {
        public int Id { get; set; }

        public int TourId { get; set; }
       
        public string UserId { get; set; }
       
        public DateTime BookingDate { get; set; }
        
    }
}
