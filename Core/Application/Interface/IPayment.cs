using Domain.Entities;

namespace Application.Interface
{
    public interface IPayment
    {
        Task<Payment?> GetByIdAsync(int id);
        Task<IEnumerable<Payment>> GetAllAsync();
        Task<IEnumerable<Payment>> GetByLoanIdAsync(int loanDisbursementId);
        Task<bool> AddAsync(Payment payment);
        Task<bool> UpdateAsync(Payment payment);
        Task<bool> DeleteAsync(int id);
        
        // Custom method to see total collections for a specific period
        Task<decimal> GetTotalCollectedAsync(DateTime start, DateTime end);
    }
}