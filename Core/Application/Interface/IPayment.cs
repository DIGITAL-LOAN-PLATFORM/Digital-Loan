using Domain.Entities;
using Application.DTO;

namespace Application.Interface
{
    public interface IPayment
    {
        Task<Payment?> GetByIdAsync(int id);
        Task<List<Payment>> GetAllPaymentsAsync();
        Task<List<Payment>> GetPaymentsByLoanDisbursementIdAsync(int loanDisbursementId);
        Task CreatePaymentAsync(CreatePaymentDTO paymentDto);
        Task UpdatePaymentAsync(int id, UpdatePaymentDTO paymentDto);
        Task DeletePaymentAsync(int id);
    }
}