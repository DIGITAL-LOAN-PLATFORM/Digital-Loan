using Domain.Entities;
using Application.DTO;

namespace Application.Interface
{
    public interface IPenalty
    {
        Task<Penalty?> GetByIdAsync(int id);
        Task<List<Penalty>> GetAllPenaltiesAsync();
        Task<List<Penalty>> GetPenaltiesByLoanDisbursementIdAsync(int loanDisbursementId);
        Task CreatePenaltyAsync(CreatePenaltyDTO penaltyDto);
        Task UpdatePenaltyAsync(int id, UpdatePenaltyDTO penaltyDto);
        Task DeletePenaltyAsync(int id);
    }
}