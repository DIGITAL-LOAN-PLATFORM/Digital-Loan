using Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPenaltyService
    {
        // The core "Day 1" engine
        Task<int> ApplyDayOnePenaltiesAsync();
        
        // Retrieval methods
        Task<PenaltyDTO?> GetPenaltyByIdAsync(int id);
        Task<IEnumerable<PenaltyDTO>> GetPenaltiesByLoanIdAsync(int loanDisbursementId);
        
        // Management Actions
        Task<bool> WaivePenaltyAsync(int penaltyId, string reason, string managerUserId);
        Task<bool> ConfirmPenaltyAsync(int penaltyId, string managerUserId);
    }
}