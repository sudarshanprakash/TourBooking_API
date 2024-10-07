using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SOSE_API.DTO;
using SOSE_API.Models;

namespace SOSE_API.Interface
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDTO> GetAllCustomers();
        CustomerDTO GetCustomerById(string id);
       // ApplicationUser AddCustomer(CustomerDTO customerDto);
        CustomerDTO UpdateCustomer(string id, CustomerDTO customerDto);
        Task<CustomerDTO> UpdateCustomerAsync(string id, CustomerDTO customerDto);
        // CustomerDTO PartialUpdateCustomer(string id, JsonPatchDocument<CustomerDTO> patchCustomer);

        Task DeleteCustomerAsync(string id);
    }
}
