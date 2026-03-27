using Domain.Entities;
using Application.Interface;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class LoanDisbursementRepository : ILoanDisbursement
    {
        private readonly ApplicationDbContext _context;

        public LoanDisbursementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LoanDisbursement?> GetByIdAsync(int id)
        {
            return await _context.LoanDisbursements
                .Include(d => d.LoanApplication)
                    .ThenInclude(la => la.Borrower)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<LoanDisbursement>> GetAllAsync()
        {
            return await _context.LoanDisbursements
                .Include(d => d.LoanApplication)
                    .ThenInclude(la => la.Borrower)
                .AsNoTracking() // Recommended for Read-only lists
                .AsSplitQuery() 
                .ToListAsync();
        }

        public async Task<IEnumerable<LoanDisbursement>> GetByLoanApplicationIdAsync(int loanApplicationId)
        {
            return await _context.LoanDisbursements
                .Where(d => d.LoanApplicationId == loanApplicationId)
                .Include(d => d.LoanApplication)
                .ToListAsync();
        }

        public async Task<bool> AddAsync(LoanDisbursement disbursement)
        {
            await _context.LoanDisbursements.AddAsync(disbursement);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(LoanDisbursement disbursement)
        {
            _context.Entry(disbursement).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var disbursement = await _context.LoanDisbursements.FindAsync(id);
            if (disbursement == null) return false;

            _context.LoanDisbursements.Remove(disbursement);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<LoanDisbursement>> GetActiveLoansAsync()
        {
            return await _context.LoanDisbursements
                .Where(d => d.Status == Domain.ValueObjects.LoanStatus.Active)
                .Include(d => d.LoanApplication)
                    .ThenInclude(la => la.Borrower)
                .ToListAsync();
        }
    }
}