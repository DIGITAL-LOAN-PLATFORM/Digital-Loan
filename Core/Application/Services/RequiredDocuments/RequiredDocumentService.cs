using Application.DTO;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services.RequiredDocuments
{
    public class RequiredDocumentService : IRequiredDocumentService
    {
        private readonly IRequiredDocument _RequiredDocument;

        
        public RequiredDocumentService(IRequiredDocument requiredDocument)
        {
            _RequiredDocument = requiredDocument;
        }

        public RequiredDocument? GetRequiredDocumentById(int id)
        {
            return _RequiredDocument.GetRequiredDocumentById(id);
        }

        public List<RequiredDocument> GetAllRequiredDocuments(FilterRequiredDocumentDTO filter)
        {
            return _RequiredDocument.GetAllRequiredDocuments(filter);
        }

        public void CreateRequiredDocument(CreateRequiredDocumentDTO requiredDocumentDTO)
        {
            _RequiredDocument.CreateRequiredDocument(requiredDocumentDTO);
        }

        public void UpdateRequiredDocument(int id, UpdateRequiredDocumentDTO requiredDocumentDTO)
        {
            _RequiredDocument.UpdateRequiredDocument(id, requiredDocumentDTO);
        }
    }
}