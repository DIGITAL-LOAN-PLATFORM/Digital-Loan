using Application.DTO;

namespace Application.Services.ProvidedDocuments
{
    public interface IProvidedDocumentService
    {
        ProvidedDocumentDTO? GetProvidedDocumentById(int id);
        List<ProvidedDocumentDTO> GetAllProvidedDocuments(FilterProvidedDocumentDTO filter);
        Task CreateProvidedDocumentAsync(CreateProvidedDocumentDTO dto);
        Task UpdateProvidedDocumentAsync(int id, UpdateProvidedDocumentDTO dto);
        void DeleteProvidedDocument(int id);
    }
}