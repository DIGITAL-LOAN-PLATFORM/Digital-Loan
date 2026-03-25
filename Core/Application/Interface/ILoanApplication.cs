using Domain.Entities;

namespace Application.Interfaces
{
    public interface ILoanApplication
    {
        
        Task<LoanApplication?> GetByIdAsync(int id);

        
        Task<IEnumerable<LoanApplication>> GetAllAsync();

        
        Task<IEnumerable<LoanApplication>> GetByStatusAsync(string status);

       
        Task<IEnumerable<LoanApplication>> GetByBorrowerIdAsync(int borrowerId);

       
        Task<LoanApplication> AddAsync(LoanApplication application);

       
        Task UpdateAsync(LoanApplication application);

        
        Task<bool> ExistsByNumberAsync(string applicationNumber);

        
        Task<int> GetCountForYearAsync(int year);
    }
}