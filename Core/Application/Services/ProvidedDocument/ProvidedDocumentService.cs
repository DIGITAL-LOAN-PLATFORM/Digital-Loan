using Application.DTO;
using Application.Interfaces;

namespace Application.Services.ProvidedDocuments
{
    public class ProvidedDocumentService : IProvidedDocumentService
    {
        private readonly IProvidedDocument _providedDocument;

        public ProvidedDocumentService(IProvidedDocument providedDocument)
        {
            _providedDocument = providedDocument;
        }

        public ProvidedDocumentDTO? GetProvidedDocumentById(int id)
        {
            return _providedDocument.GetProvidedDocumentById(id);
        }

        public List<ProvidedDocumentDTO> GetAllProvidedDocuments(FilterProvidedDocumentDTO filter)
        {
            return _providedDocument.GetAllProvidedDocuments(filter);
        }

        public async Task CreateProvidedDocumentAsync(CreateProvidedDocumentDTO dto)
        {
            await _providedDocument.CreateProvidedDocumentAsync(dto);
        }

        public async Task UpdateProvidedDocumentAsync(int id, UpdateProvidedDocumentDTO dto)
        {
            await _providedDocument.UpdateProvidedDocumentAsync(id, dto);
        }

        public void DeleteProvidedDocument(int id)
        {
            _providedDocument.DeleteProvidedDocument(id);
        }
    }
}