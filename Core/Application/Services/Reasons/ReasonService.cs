using Domain.Entities;
using Application.Interface;
using Application.DTO;

namespace Application.Services.Reasons
{
    public class ReasonService : IReasonService
    {
        private readonly IReason reasonRepository;

        public ReasonService(IReason reasonRepository)
        {
            this.reasonRepository = reasonRepository;
        }

        public async Task<Reason?> GetByIdAsync(int id)
        {
            return await reasonRepository.GetByIdAsync(id);
        }

        public async Task<List<Reason>> GetAllReasonsAsync()
        {
            return await reasonRepository.GetAllReasonsAsync();
        }

        public async Task CreateReasonAsync(CreateReasonDTO reasonDto)
        {
            await reasonRepository.CreateReasonAsync(reasonDto);
        }

        public async Task UpdateReasonAsync(int id, UpdateReasonDTO reasonDto)
        {
            await reasonRepository.UpdateReasonAsync(id, reasonDto);
        }

        public async Task DeleteReasonAsync(int id)
        {
            await reasonRepository.DeleteReasonAsync(id);
        }
    }
}