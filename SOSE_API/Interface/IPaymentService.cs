using Microsoft.AspNetCore.JsonPatch;
using SOSE_API.DTO;
using SOSE_API.Models;

namespace SOSE_API.Interface
{
    public interface IPaymentService
    {

        IEnumerable<Payment> GetAllPayments();
        Payment GetPaymentId(int id);
        Payment AddPayment(PaymentDTO paymentDto);
        Payment UpdatePayment(int id, PaymentDTO PaymentDto);
        Booking PartialUpdatePayment(int id, JsonPatchDocument<PaymentDTO> patchPayment);

        void DeleteTour(int id);
    }
}
