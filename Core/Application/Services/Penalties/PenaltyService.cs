using Domain.Entities;
using Application.Interface;
using Application.DTO;

namespace Application.Services.Penalties
{
    public class PenaltyService : IPenaltyService
    {
        private readonly IPenalty penaltyRepository;

        public PenaltyService(IPenalty penaltyRepository)
        {
            this.penaltyRepository = penaltyRepository;
        }

        public async Task<Penalty?> GetByIdAsync(int id)
        {
            return await penaltyRepository.GetByIdAsync(id);
        }

        public async Task<List<Penalty>> GetAllPenaltiesAsync()
        {
            return await penaltyRepository.GetAllPenaltiesAsync();
        }

        public async Task<List<Penalty>> GetPenaltiesByLoanDisbursementIdAsync(int loanDisbursementId)
        {
            return await penaltyRepository.GetPenaltiesByLoanDisbursementIdAsync(loanDisbursementId);
        }

        public async Task CreatePenaltyAsync(CreatePenaltyDTO penaltyDto)
        {
            await penaltyRepository.CreatePenaltyAsync(penaltyDto);
        }

        public async Task UpdatePenaltyAsync(int id, UpdatePenaltyDTO penaltyDto)
        {
            await penaltyRepository.UpdatePenaltyAsync(id, penaltyDto);
        }

        public async Task DeletePenaltyAsync(int id)
        {
            await penaltyRepository.DeletePenaltyAsync(id);
        }
    }
}