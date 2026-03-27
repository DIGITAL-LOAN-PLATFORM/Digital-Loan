using Application.DTO;
using Domain.Entities;

namespace Application.Services.RequiredDocuments
{
    public interface IRequiredDocumentService
    {
        RequiredDocument GetRequiredDocumentById(int id);
        List<RequiredDocument> GetAllRequiredDocuments(FilterRequiredDocumentDTO filter);  
        void CreateRequiredDocument(CreateRequiredDocumentDTO RequiredDocumentDTO);         
        void UpdateRequiredDocument(int id, UpdateRequiredDocumentDTO RequiredDocumentDTO); 
    }
}