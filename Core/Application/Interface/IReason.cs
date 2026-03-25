using Domain.Entities;
using Application.DTO;

namespace Application.Interface
{
    public interface IReason
    {
        Task<Reason?> GetByIdAsync(int id);
        Task<List<Reason>> GetAllReasonsAsync();
        Task CreateReasonAsync(CreateReasonDTO reasonDto);
        Task UpdateReasonAsync(int id, UpdateReasonDTO reasonDto);
        Task DeleteReasonAsync(int id);
    }
}