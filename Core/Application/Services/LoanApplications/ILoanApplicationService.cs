using Application.DTO;

namespace Application.Interfaces
{
    public interface ILoanApplicationService
    {
      
        Task<IEnumerable<LoanApplicationDTO>> GetAllApplicationsAsync();
        Task<LoanApplicationDTO?> GetApplicationByIdAsync(int id);
        Task<IEnumerable<LoanApplicationDTO>> GetPendingApplicationsAsync();

      
        Task<LoanApplicationDTO> CreateApplicationAsync(LoanApplicationDTO applicationDto);
        Task<bool> UpdateApplicationStatusAsync(int id, string newStatus, string? remarks = null);
        
        
        Task<string> GenerateNewApplicationNumberAsync();
    }
}