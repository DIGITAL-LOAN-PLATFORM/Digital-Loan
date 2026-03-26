using Application.DTO;
using Application.Interface;
using Domain.Entities;

namespace Application.Services.DocumentTypes
{
    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly IDocumentType _documentType;

        public DocumentTypeService(IDocumentType documentType)
        {
            _documentType = documentType;
        }

        public List<DocumentType> GetAllDocumentTypes(FilterDocumentTypeDTO filter)
            => _documentType.GetAllDocumentTypes(filter);

        public DocumentType? GetDocumentTypeById(int id)
            => _documentType.GetDocumentTypeById(id);

        public void CreateDocumentType(CreateDocumentTypeDTO dto)
            => _documentType.CreateDocumentType(dto);

        public void UpdateDocumentType(int id, UpdateDocumentTypeDTO dto)
            => _documentType.UpdateDocumentType(id, dto);

        public void DeleteDocumentType(int id)
            => _documentType.DeleteDocumentType(id);
    }
}