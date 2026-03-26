using Application.DTO;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services.RequiredDocuments
{
    public class RequiredDocumentService : IRequiredDocumentService
    {
        private readonly IRequiredDocument _requiredDocument;

        public RequiredDocumentService(IRequiredDocument requiredDocument)
        {
            _requiredDocument = requiredDocument;
        }

        public RequiredDocument? GetRequiredDocumentById(int id)
        {
            return _requiredDocument.GetRequiredDocumentById(id);
        }

        public List<RequiredDocument> GetAllRequiredDocuments(FilterRequiredDocumentDTO filter)
        {
            return _requiredDocument.GetAllRequiredDocuments(filter);
        }

        public void CreateRequiredDocument(CreateRequiredDocumentDTO dto)
        {
            _requiredDocument.CreateRequiredDocument(dto);
        }

        public void UpdateRequiredDocument(int id, UpdateRequiredDocumentDTO dto)
        {
            _requiredDocument.UpdateRequiredDocument(id, dto);
        }
    }
}