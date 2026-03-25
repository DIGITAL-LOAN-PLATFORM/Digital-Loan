using Domain.Entities;
using Application.DTO;

namespace Application.Interface
{
    public interface IDocumentType
    {
        List<DocumentType> GetAllDocumentTypes(FilterDocumentTypeDTO filter);
        DocumentType? GetDocumentTypeById(int id);
        void CreateDocumentType(CreateDocumentTypeDTO dto);
        void UpdateDocumentType(int id, UpdateDocumentTypeDTO dto);
        void DeleteDocumentType(int id);
    }
}