namespace SOSE_API.DTO
{
    public class RegisterRequestDTO
    {
         public string UserName  { get; set; }
        public string FullName { get; set; }

        public string Password {  get; set; }
        public string Role { get; set; }    
    }
}
