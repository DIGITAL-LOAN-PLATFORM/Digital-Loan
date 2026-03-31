using Application.DTO;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        // FIX: Change RepaymentScheduleItemDTO to RepaymentScheduleItem
        Task<IEnumerable<RepaymentScheduleItem>> GetNewlyOverdueInstallmentsAsync();
        
        // FIX: Change RepaymentScheduleItemDTO to RepaymentScheduleItem
        Task<bool> UpdateScheduleItemAsync(RepaymentScheduleItem item);
        Task<LoanDisbursement?> GetByIdWithPenaltiesAsync(int id);
    }
}