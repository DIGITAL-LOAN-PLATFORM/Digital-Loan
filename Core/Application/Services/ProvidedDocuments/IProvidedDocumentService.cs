using Application.DTO;

namespace Application.Services
{
    public interface IProvidedDocumentService
    {
        Task<ProvidedDocumentDTO?> GetDocumentByIdAsync(int id);
        Task<IEnumerable<ProvidedDocumentDTO>> GetDocumentsByLoanApplicationAsync(int loanApplicationId);
        Task<IEnumerable<ProvidedDocumentDTO>> GetDocumentsByLoanDisbursementAsync(int loanDisbursementId);
        Task<IEnumerable<ProvidedDocumentDTO>> GetAllDocumentsAsync();
        Task<bool> UploadDocumentAsync(CreateProvidedDocumentDTO dto);
        Task<bool> UpdateDocumentStatusAsync(int id, string status, string? remarks);
        Task<bool> DeleteDocumentAsync(int id);
    }
}
