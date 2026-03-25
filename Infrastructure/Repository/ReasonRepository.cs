using Application.Interface;
using Application.DTO;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ReasonRepository : IReason
    {
        private readonly ApplicationDbContext _dbContext;

        public ReasonRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task<Reason?> GetByIdAsync(int id)
        {
            return await _dbContext.Reasons.FindAsync(id);
        }

        public async Task<List<Reason>> GetAllReasonsAsync()
        {
            return await _dbContext.Reasons.ToListAsync();
        }

        public async Task CreateReasonAsync(CreateReasonDTO reasonDto)
        {
            var newReason = new Reason
            {
                Name = reasonDto.Name
            };

            await _dbContext.Reasons.AddAsync(newReason);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateReasonAsync(int id, UpdateReasonDTO reasonDto)
        {
            var existingReason = await _dbContext.Reasons.FindAsync(id);
            if (existingReason == null) return;

            existingReason.Name = reasonDto.Name;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteReasonAsync(int id)
        {
            var reason = await _dbContext.Reasons.FindAsync(id);
            if (reason == null) return;

            _dbContext.Reasons.Remove(reason);
            await _dbContext.SaveChangesAsync();
        }
    }
}