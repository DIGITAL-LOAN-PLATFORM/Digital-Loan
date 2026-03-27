using Domain.Entities;

namespace Application.Interface
{
    public interface ILoanDisbursement
    {
        Task<LoanDisbursement?> GetByIdAsync(int id);
        Task<IEnumerable<LoanDisbursement>> GetAllAsync();
        Task<IEnumerable<LoanDisbursement>> GetByLoanApplicationIdAsync(int loanApplicationId);
        Task<bool> AddAsync(LoanDisbursement disbursement);
        Task<bool> UpdateAsync(LoanDisbursement disbursement);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<LoanDisbursement>> GetActiveLoansAsync();
    }
}