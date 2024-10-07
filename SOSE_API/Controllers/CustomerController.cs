using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SOSE_API.Data;
using SOSE_API.DTO;
using SOSE_API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SOSE_API.Interface;


namespace SOSE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var customers = _customerService.GetAllCustomers();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        //[HttpPost]
        //public IActionResult CreateCustomer([FromBody] CustomerDTO customer)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var InsertedCustomer =_customerService.AddCustomer(customer);
        //    return Ok(InsertedCustomer);
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerAsync(string id, CustomerDTO customerDto)
        {
            if (!ModelState.IsValid || id != customerDto.ID)
            {
                return BadRequest();
            }

            var updatedCustomer= await _customerService.UpdateCustomerAsync(id, customerDto);
            return Ok(updatedCustomer);
        }


        //[HttpPatch]
        //public IActionResult partialUpdateCustomer(string id, [FromBody] JsonPatchDocument<CustomerDTO> patchCustomer)


        //{
        //    if (patchCustomer == null || id == null)
        //    {
        //        return BadRequest();
        //    }
            

        //    var patchedcustomer = _customerService.PartialUpdateCustomer(id, patchCustomer);


        //    return Ok(patchedcustomer);

        //}


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
           await _customerService.DeleteCustomerAsync(id);
            return Ok("Deleted succssfully ");
        }
    }

}
