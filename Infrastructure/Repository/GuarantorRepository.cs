using Domain.Entities;
using Application.Interface;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GuarantorRepository : IGuarantor
    {
        private readonly ApplicationDbContext _context;

        public GuarantorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guarantor?> GetByIdAsync(int id)
        {
            return await _context.Guarantors
                .Include(g => g.GuarantorType)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Guarantor>> GetByLoanApplicationIdAsync(int loanApplicationId)
        {
            return await _context.Guarantors
                .Include(g => g.GuarantorType)
                .Where(g => g.LoanApplicationId == loanApplicationId)
                .ToListAsync();
        }

        public async Task<Guarantor?> GetByIdentificationNumberAsync(string idNumber)
        {
            return await _context.Guarantors
                .Include(g => g.GuarantorType)
                .FirstOrDefaultAsync(g => g.IdentificationNumber == idNumber);
        }

        public async Task<IEnumerable<Guarantor>> GetAllAsync()
        {
            return await _context.Guarantors
                .Include(g => g.GuarantorType)
                .Include(g => g.LoanApplication)
                .ToListAsync();
        }

        public async Task<Guarantor> AddAsync(Guarantor guarantor)
        {
            try
            {
                await _context.Guarantors.AddAsync(guarantor);
                await _context.SaveChangesAsync();
                return guarantor;
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException($"Failed to add guarantor: {ex.InnerException?.Message}", ex);
            }
        }

        public async Task UpdateAsync(Guarantor guarantor)
        {
            try
            {
                _context.Entry(guarantor).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException($"Failed to update guarantor: {ex.InnerException?.Message}", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var guarantor = await _context.Guarantors.FindAsync(id);
            if (guarantor != null)
            {
                _context.Guarantors.Remove(guarantor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsAlreadyGuarantorAsync(int loanApplicationId, string idNumber)
        {
            return await _context.Guarantors
                .AnyAsync(g => g.LoanApplicationId == loanApplicationId && 
                               g.IdentificationNumber == idNumber);
        }
    }
}

