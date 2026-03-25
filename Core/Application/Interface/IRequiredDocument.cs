using Domain.Entities;
using Application.DTO;

namespace Application.Interfaces
{
    public interface IRequiredDocument
    {
        List<RequiredDocument> GetAllRequiredDocuments(FilterRequiredDocumentDTO filterDTO);
        RequiredDocument? GetRequiredDocumentById(int id); 
        void CreateRequiredDocument(CreateRequiredDocumentDTO RequiredDocumentDTO);
        void UpdateRequiredDocument(int id, UpdateRequiredDocumentDTO RequiredDocumentDTO);
    }
}