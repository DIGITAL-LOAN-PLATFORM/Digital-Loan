using Application.Interfaces;
using Application.DTO;
using Domain.Entities;

namespace Application.Services.RequiredDocuments
{
    public interface IRequiredDocumentService
    {
        Task<List<RequiredDocument>> GetAllRequiredDocumentsAsync(FilterRequiredDocumentDTO? filter = null);
        Task<RequiredDocument?> GetRequiredDocumentByIdAsync(int id);
        Task<int> CreateRequiredDocumentAsync(CreateRequiredDocumentDTO dto);
        Task UpdateRequiredDocumentAsync(int id, UpdateRequiredDocumentDTO dto);
        Task DeleteRequiredDocumentAsync(int id);
    }
}
