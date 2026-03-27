using Application.DTO;

namespace Application.Services
{
    public interface IGuarantorService
    {
        Task<IEnumerable<GuarantorDTO>> GetGuarantorsByLoanAsync(int loanApplicationId);
        Task<GuarantorDTO?> GetGuarantorByIdAsync(int id);
        
        // This matches your CreateGuarantorDTO
Task<Result<GuarantorDTO>> AddGuarantorAsync(CreateGuarantorDTO dto);
        
        // This matches your UpdateGuarantorDTO
Task<Result<GuarantorDTO>> UpdateGuarantorAsync(int id, UpdateGuarantorDTO dto);
        
Task<Result<bool>> RemoveGuarantorAsync(int id);
        
        Task<IEnumerable<GuarantorDTO>> GetAllGuarantorsAsync();
Task<Result<bool>> DeleteGuarantorAsync(int id);
    }
}
