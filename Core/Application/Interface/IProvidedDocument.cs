using Application.DTO;

namespace Application.Interface
{
    public interface IProvidedDocument
    {
        Task<ProvidedDocumentDTO?> GetByIdAsync(int id);
        Task<IEnumerable<ProvidedDocumentDTO>> GetByLoanApplicationIdAsync(int loanApplicationId);
        Task<IEnumerable<ProvidedDocumentDTO>> GetByLoanDisbursementIdAsync(int loanDisbursementId);
        Task<IEnumerable<ProvidedDocumentDTO>> GetAllAsync();
        Task<bool> AddAsync(Domain.Entities.ProvidedDocument document);
        Task<bool> UpdateAsync(Domain.Entities.ProvidedDocument document);
        Task<bool> DeleteAsync(int id);
    }
}
