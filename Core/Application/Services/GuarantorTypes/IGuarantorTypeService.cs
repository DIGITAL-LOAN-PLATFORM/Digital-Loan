using Application.DTO;
using Domain.Entities;

namespace Application.Services
{
    public interface IGuarantorTypeService
    {
         Task<List<GuarantorType>> GetAllGuarantorTypeAsync();
        Task<GuarantorType?> GetByIdAsync(int id);
        Task CreateGuarantorTypeAsync(CreateGuarantorTypeDTO guarantorTypeDto);
        Task UpdateGuarantorAsync(int id, UpdateGuarantorTypeDTO guarantorTypeDto);
    }

   
}