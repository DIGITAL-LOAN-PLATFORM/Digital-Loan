using Domain.Entities;
using Application.Interface;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class LoanApplicationRepository : ILoanApplication
    {
        private readonly ApplicationDbContext _context;

        public LoanApplicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<LoanApplication?> GetByIdAsync(int id)
        {
            return await _context.LoanApplications
                .Include(l => l.Borrower)
                .Include(l => l.loanProduct)
                .Include(l => l.paymentModality)
                .Include(l => l.Guarantors)
                .Include(l => l.ProvidedDocuments)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        
        public async Task<IEnumerable<LoanApplication>> GetAllAsync()
        {
            return await _context.LoanApplications
                .Include(l => l.Borrower)
                .Include(l => l.loanProduct)
                .OrderByDescending(l => l.DateOfApplication)
                .ToListAsync();
        }

        
        public async Task<IEnumerable<LoanApplication>> GetByStatusAsync(string status)
        {
            return await _context.LoanApplications
                .Include(l => l.Borrower)
                .Include(l => l.loanProduct)
                .Where(l => l.Status == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<LoanApplication>> GetByBorrowerIdAsync(int borrowerId)
        {
            return await _context.LoanApplications
                .Include(l => l.loanProduct)
                .Where(l => l.BorrowerId == borrowerId)
                .ToListAsync();
        }

      
        public async Task<LoanApplication> AddAsync(LoanApplication application)
        {
            _context.LoanApplications.Add(application);
            await _context.SaveChangesAsync();
            return application;
        }

        public async Task UpdateAsync(LoanApplication application)
        {
            _context.Entry(application).State = EntityState.Modified;
            
            
            await _context.SaveChangesAsync();
        }

        
        public async Task<bool> ExistsByNumberAsync(string applicationNumber)
        {
            return await _context.LoanApplications
                .AnyAsync(l => l.ApplicationNumber == applicationNumber);
        }

        public async Task<int> GetCountForYearAsync(int year)
        {
            return await _context.LoanApplications
                .CountAsync(l => l.DateOfApplication.Year == year);
        }
    }
}