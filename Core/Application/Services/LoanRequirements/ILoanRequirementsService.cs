using Application.DTO;

namespace Application.Services.LoanRequirements
{
    public interface ILoanRequirementsService
    {
        Task<List<LoanRequirementsDTO>> GetAllRequirementsAsync();
        Task<LoanRequirementsDTO?> GetRequirementByIdAsync(int id);
        Task<List<LoanRequirementsDTO>> GetRequirementsByProductIdAsync(int productId);
        Task<bool> CreateRequirementAsync(CreateLoanRequirementsDTO dto);
        Task<bool> UpdateRequirementAsync(int id, UpdateLoanRequirementsDTO dto);
        Task<bool> DeleteRequirementAsync(int id);
    }
}
