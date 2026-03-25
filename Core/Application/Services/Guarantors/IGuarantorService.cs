using Application.DTO;

namespace Application.Services
{
    public interface IGuarantorService
    {
        Task<IEnumerable<GuarantorDTO>> GetGuarantorsByLoanAsync(int loanApplicationId);
        Task<GuarantorDTO?> GetGuarantorByIdAsync(int id);
        
        // This matches your CreateGuarantorDTO
        Task<bool> AddGuarantorAsync(CreateGuarantorDTO dto);
        
        // This matches your UpdateGuarantorDTO
        Task<bool> UpdateGuarantorAsync(int id, UpdateGuarantorDTO dto);
        
        Task<bool> RemoveGuarantorAsync(int id);
    }
}