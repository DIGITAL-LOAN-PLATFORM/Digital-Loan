using Application.DTO;

namespace Application.Interfaces
{
    public interface IProvidedDocument
    {
        ProvidedDocumentDTO? GetProvidedDocumentById(int id);
        List<ProvidedDocumentDTO> GetAllProvidedDocuments(FilterProvidedDocumentDTO filter);
        Task CreateProvidedDocumentAsync(CreateProvidedDocumentDTO dto);
        Task UpdateProvidedDocumentAsync(int id, UpdateProvidedDocumentDTO dto);
        void DeleteProvidedDocument(int id);
    }
}