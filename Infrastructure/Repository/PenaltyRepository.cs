using Application.Interface;
using Application.DTO;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PenaltyRepository : IPenalty
    {
        private readonly ApplicationDbContext _dbContext;

        public PenaltyRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task<Penalty?> GetByIdAsync(int id)
        {
            return await _dbContext.Penalties
                .Include(p => p.LoanDisbursement)
                .Include(p => p.PenaltyReason)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Penalty>> GetAllPenaltiesAsync()
        {
            return await _dbContext.Penalties
                .Include(p => p.LoanDisbursement)
                .Include(p => p.PenaltyReason)
                .ToListAsync();
        }

        public async Task<List<Penalty>> GetPenaltiesByLoanDisbursementIdAsync(int loanDisbursementId)
        {
            return await _dbContext.Penalties
                .Include(p => p.LoanDisbursement)
                .Include(p => p.PenaltyReason)
                .Where(p => p.LoanDisbursementId == loanDisbursementId)
                .ToListAsync();
        }

        public async Task CreatePenaltyAsync(CreatePenaltyDTO penaltyDto)
        {
            var newPenalty = new Penalty
            {
                LoanDisbursementId = penaltyDto.LoanDisbursementId,
                PenaltyAmount = penaltyDto.PenaltyAmount,
                ReasonId = penaltyDto.ReasonId,
                ConfirmedByUserId = penaltyDto.ConfirmedByUserId,
                CreatedAt = DateTime.UtcNow
            };

            await _dbContext.Penalties.AddAsync(newPenalty);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePenaltyAsync(int id, UpdatePenaltyDTO penaltyDto)
        {
            var existingPenalty = await _dbContext.Penalties.FindAsync(id);
            if (existingPenalty == null) return;

            existingPenalty.PenaltyAmount = penaltyDto.PenaltyAmount;
            existingPenalty.ReasonId = penaltyDto.ReasonId;
            existingPenalty.ConfirmedByUserId = penaltyDto.ConfirmedByUserId;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePenaltyAsync(int id)
        {
            var penalty = await _dbContext.Penalties.FindAsync(id);
            if (penalty == null) return;

            _dbContext.Penalties.Remove(penalty);
            await _dbContext.SaveChangesAsync();
        }
    }
}