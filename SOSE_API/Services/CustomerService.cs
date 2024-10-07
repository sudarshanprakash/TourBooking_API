using Microsoft.AspNetCore.JsonPatch;
using SOSE_API.DTO;
using SOSE_API.Interface;
using SOSE_API.Models;
using System.Web.Http.ModelBinding;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SOSE_API.Data;
using SOSE_API.DTO;
using SOSE_API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace SOSE_API.Services
{
    public class CustomerService:ICustomerService
    {

        private readonly IRepository<ApplicationUser> _customerRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomerService(IRepository<ApplicationUser> customerRepository, UserManager<ApplicationUser> userManager)
        {
            _customerRepository = customerRepository;
            _userManager = userManager;
        }

        public IEnumerable< CustomerDTO> GetAllCustomers()
        {
            return _customerRepository.GetAll(c => c.Booking)
         .Select(c => new CustomerDTO
         {
             ID = c.Id,
             FullName = c.FullName,
             UserName = c.UserName,
             Phone=c.Phone

             // Add other properties from CustomerDTO as needed
         });
        }

        public CustomerDTO GetCustomerById(string id)
        {
            var customer =  _customerRepository.GetByIdStr(id, c => c.Booking);
            if (customer == null) return null;

            return new CustomerDTO
            {
                ID=customer.Id,
                FullName=customer.FullName,
                UserName=customer.UserName,
                Phone=customer.Phone


            };
        }

        //public CustomerDTO AddCustomer(CustomerDTO customerDto)
        //{
        //    var User = new ApplicationUser
        //    {
        //        FullName = customerDto.FullName,
        //         Phone = customerDto.Email
        //    };

        //    _customerRepository.Insert(customer);
        //    _customerRepository.Save();
        //    return customer;
        //}

        public async Task<CustomerDTO> UpdateCustomerAsync(string id, CustomerDTO customerDto)
        {
            // Retrieve the existing user from the UserManager
            var existingUser = await _userManager.FindByIdAsync(id);
            if (existingUser == null)
            {
                throw new Exception($"User with ID {id} not found.");
            }

            // Update user properties
            existingUser.FullName = customerDto.FullName;
            existingUser.Phone = customerDto.Phone; // Make sure you're using the correct property
            existingUser.UserName = customerDto.UserName;

            try
            {
                // Update the user in the UserManager
                var result = await _userManager.UpdateAsync(existingUser);
                if (!result.Succeeded)
                {
                    // Handle errors (you can log them or throw a custom exception)
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new Exception($"Failed to update user: {errors}");
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Log the concurrency exception (implement your logging here)
                throw new Exception("Concurrency conflict occurred while updating the user.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions (e.g., logging, rethrowing)
                throw new Exception("An error occurred while updating the user.", ex);
            }

            // Return the updated CustomerDTO
            return new CustomerDTO
            {
                ID = existingUser.Id,
                FullName = existingUser.FullName,
                Phone = existingUser.Phone,
                UserName = existingUser.UserName
            };
        }

        public CustomerDTO UpdateCustomer(string id, CustomerDTO customerDto)
        {
            var customer = _customerRepository.GetByIdStr(id);
            if (customer == null) return null;

            ApplicationUser newCustomer = new ApplicationUser()
            {
                Id=customerDto.ID,
                FullName = customerDto.FullName,
                Phone = customerDto.Phone,
                UserName= customerDto.UserName, 
               

            };

           

            _customerRepository.Update(newCustomer);
            _customerRepository.Save();

            return new CustomerDTO
            {
                ID=newCustomer.Id,
                FullName=newCustomer.FullName,
                Phone=newCustomer.Phone,
                UserName=newCustomer.UserName
            };
        }

        //public CustomerDTO PartialUpdateCustomer(string id, JsonPatchDocument<CustomerDTO> patchCustomer)
        //{
        //    // Retrieve the existing customer entity
        //    var customerEntity = _customerRepository.GetByIdStr(id);
        //    if (customerEntity == null)
        //    {
        //        throw new ArgumentNullException(nameof(customerEntity), "Customer not found");
        //    }

        //    // Map entity to DTO to apply the patch
        //    CustomerDTO customerDto = new()
        //    {
        //        ID = customerEntity.Id,
        //        FullName = customerEntity.FullName,
        //        Phone = customerEntity.Phone,
        //        UserName = customerEntity.UserName,
        //    };

        //    // Apply the patch to the DTO
        //    patchCustomer.ApplyTo(customerDto);

        //    // Map the modified DTO back to the entity

        //    customerEntity.FullName = customerDto.FullName;
        //    customerEntity.Phone = customerDto.Phone;
        //    customerEntity.UserName = customerDto.UserName; 
        //    //customerEntity.Email = customerDto.Email;

        //    // Update the entity in the database
        //    _customerRepository.Update(customerEntity);
        //    _customerRepository.Save();
        //    return new CustomerDTO
        //    {
        //        ID=customerEntity.Id,
        //        FullName=customerEntity.FullName,
        //        Phone=customerEntity.Phone,
        //        UserName=customerEntity.UserName,   
        //    };
        //}

        public async Task DeleteCustomerAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    // User deleted successfully
                    return;
                }
                else
                {
                    // Handle failure
                    throw new Exception("Failed to delete the user.");
                }
            }
            else
            {
                throw new Exception("User not found.");
            }
        }

    }
}
