using Domain.Entities;
using Application.Interface;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PaymentRepository : IPayment
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Payment?> GetByIdAsync(int id)
        {
            return await _context.Payments
                .Include(p => p.PaymentType)
                .Include(p => p.Account)
                .Include(p => p.LoanDisbursement)
                    .ThenInclude(ld => ld.LoanApplication)
                        .ThenInclude(la => la.Borrower)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

      public async Task<IEnumerable<Payment>> GetAllAsync()
{
    return await _context.Payments
        .Include(p => p.PaymentType)
        .Include(p => p.LoanDisbursement)
            .ThenInclude(ld => ld.LoanApplication)
                .ThenInclude(la => la.Borrower) // CRITICAL: This was missing
        .OrderByDescending(p => p.PaymentDate)
        .AsNoTracking()
        .ToListAsync();
}

        public async Task<IEnumerable<Payment>> GetByLoanIdAsync(int loanDisbursementId)
        {
            // This is used by your PaymentService to show history for a specific loan
            return await _context.Payments
                .Where(p => p.LoanDisbursementId == loanDisbursementId)
                .Include(p => p.PaymentType)
                .OrderByDescending(p => p.PaymentDate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> AddAsync(Payment payment)
        {
            // This is triggered by ProcessLoanPaymentAsync in your service
            await _context.Payments.AddAsync(payment);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Payment payment)
        {
            _context.Entry(payment).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null) return false;

            _context.Payments.Remove(payment);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<decimal> GetTotalCollectedAsync(DateTime start, DateTime end)
        {
            return await _context.Payments
                .Where(p => p.PaymentDate >= start && p.PaymentDate <= end)
                .SumAsync(p => p.TotalAmountPaid);
        }
    }
}