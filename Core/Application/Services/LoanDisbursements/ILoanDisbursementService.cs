using Application.DTO;

namespace Application.Services
{
    public interface ILoanDisbursementService
    {
        Task<LoanDisbursementDTO?> GetDisbursementByIdAsync(int id);
        Task<IEnumerable<LoanDisbursementDTO>> GetAllDisbursementsAsync();
        Task<IEnumerable<LoanDisbursementDTO>> GetDisbursementsByLoanIdAsync(int loanApplicationId);
        
        // This is the main "Action" method
        Task<bool> DisburseLoanAsync(CreateLoanDisbursementDTO createDto);
        
        Task<bool> UpdateDisbursementAsync(int id, UpdateLoanDisbursementDTO updateDto);
    }
}