using Application.DTO;
using Domain.Entities;

namespace Application.Services.DocumentTypes
{
    public interface IDocumentTypeService
    {
        List<DocumentType> GetAllDocumentTypes(FilterDocumentTypeDTO filter);
        DocumentType? GetDocumentTypeById(int id);
        void CreateDocumentType(CreateDocumentTypeDTO dto);
        void UpdateDocumentType(int id, UpdateDocumentTypeDTO dto);
        void DeleteDocumentType(int id);
    }
}