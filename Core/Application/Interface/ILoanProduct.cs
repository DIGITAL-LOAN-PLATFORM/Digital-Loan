using Application.DTO;
using Domain.Entities;

namespace Application.Interface
{
    public interface ILoanProduct
    {
        
        Task<List<LoanProduct>> GetAllAsync();

        
        Task<LoanProduct?> GetByIdAsync(int id);

        
        Task<LoanProduct> CreateAsync(LoanProductDTO loanProductDto);

       
        Task UpdateAsync(int id, LoanProductDTO loanProductDto);

        Task DeleteAsync(int id);

        
        Task<bool> ExistsAsync(string productName);
    }
}