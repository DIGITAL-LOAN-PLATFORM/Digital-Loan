using Application.DTO;
using Domain.Entities;

namespace Application.Interface
{
    public interface ILoanRequirements
    {
        Task<List<LoanRequirements>> GetAllAsync();
        Task<LoanRequirements?> GetByIdAsync(int id);
        Task<List<LoanRequirements>> GetByProductIdAsync(int productId);
        Task<LoanRequirements> CreateAsync(CreateLoanRequirementsDTO dto);
        Task UpdateAsync(int id, UpdateLoanRequirementsDTO dto);
        Task DeleteAsync(int id);
        Task<bool> ExistsByNameAsync(string name);
    }
}
