using SOSE_API.Models;
using System.ComponentModel.DataAnnotations;

namespace SOSE_API.DTO
{
    public class GetTourDTO
    {
        
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

      
        public ICollection<ReviewDTO> Review { get; set; }
    }
}
