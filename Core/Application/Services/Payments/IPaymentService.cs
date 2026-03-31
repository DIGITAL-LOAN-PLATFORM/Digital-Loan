using Application.DTO;

namespace Application.Interface
{
    public interface IPaymentService
    {
        Task<PaymentDTO?> GetPaymentByIdAsync(int id);
        Task<IEnumerable<PaymentDTO>> GetPaymentsByLoanIdAsync(int loanId);
        
        // This is the core method for the Waterfall logic
        Task<bool> ProcessLoanPaymentAsync(PaymentCreateDTO paymentDto);
        
        Task<bool> UpdatePaymentDetailsAsync(PaymentUpdateDTO updateDto);
        Task<IEnumerable<PaymentDTO>> GetAllPaymentsAsync();
    }
}