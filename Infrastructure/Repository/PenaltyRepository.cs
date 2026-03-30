using Domain.Entities;
using Domain.Interface;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PenaltyRepository : IPenalty
    {
        private readonly ApplicationDbContext _context;

        public PenaltyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Penalty?> GetByIdAsync(int id)
        {
            return await _context.Penalties
                .Include(p => p.PenaltyReason)
                .Include(p => p.LoanDisbursement)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Penalty>> GetByLoanIdAsync(int loanDisbursementId)
        {
            return await _context.Penalties
                .Where(p => p.LoanDisbursementId == loanDisbursementId)
                .OrderByDescending(p => p.EventDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Penalty>> GetUnpaidPenaltiesByLoanIdAsync(int loanDisbursementId)
        {
            // Selects only Active penalties where there is still money owed
            // This is the "Queue" for the Payment Waterfall
            return await _context.Penalties
                .Where(p => p.LoanDisbursementId == loanDisbursementId && 
                            p.Status == PenaltyStatus.Active && 
                            p.CurrentBalance > 0)
                .OrderBy(p => p.EventDate) // Oldest penalties get paid first
                .ToListAsync();
        }

        public async Task<bool> AddAsync(Penalty penalty)
        {
            // Ensure CurrentBalance matches the initial PenaltyAmount on creation
            penalty.CurrentBalance = penalty.PenaltyAmount - penalty.AmountPaid;
            
            await _context.Penalties.AddAsync(penalty);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Penalty penalty)
        {
            // Recalculate balance before saving to prevent manual math errors
            penalty.CurrentBalance = penalty.PenaltyAmount - penalty.AmountPaid;
            
            // Auto-set status to Paid if balance hits zero
            if (penalty.CurrentBalance <= 0 && penalty.Status == PenaltyStatus.Active)
            {
                penalty.Status = PenaltyStatus.Paid;
            }

            _context.Penalties.Update(penalty);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> PenaltyExistsForDateAsync(int loanDisbursementId, DateTime eventDate)
        {
            // Prevents the system from double-charging a penalty for the same late day
            return await _context.Penalties.AnyAsync(p => 
                p.LoanDisbursementId == loanDisbursementId && 
                p.EventDate.Date == eventDate.Date &&
                p.Status != PenaltyStatus.Waived);
        }
      public async Task<IEnumerable<RepaymentScheduleItem>> GetNewlyOverdueInstallmentsAsync()
{
    return await _context.RepaymentScheduleItems
        .Where(x => x.DueDate < DateTime.Today && 
                    !x.IsPaid && 
                    x.LastPenaltyDate == null) 
        .ToListAsync();
}
    }
}