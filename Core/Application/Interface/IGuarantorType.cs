using Application.DTO;
using Domain.Entities;

namespace Application.Interface
{
    public interface IGuarantorType
    {
         Task<List<GuarantorType>> GetAllGuarantorTypeAsync();
        Task<GuarantorType?> GetByIdAsync(int id);
        Task CreateGuarantorTypeAsync(CreateGuarantorTypeDTO guarantorTypeDto);
        Task UpdateBorrowerAsync(int id, UpdateGuarantorTypeDTO guarantorTypeDto);
    }

   
}