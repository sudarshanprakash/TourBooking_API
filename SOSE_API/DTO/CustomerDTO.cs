using System.ComponentModel.DataAnnotations;

namespace SOSE_API.DTO
{
    public class CustomerDTO
    {
        [Key]
        public string ID { get; set; }
        public string FullName { get; set; }

        public int Phone { get; set; }

        public string UserName {  get; set; }   
    }
}
