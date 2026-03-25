using Application.DTO;
using Application.Interface; 
using Domain.Entities;


namespace Application.Services
{
    public class GuarantorTypeService : IGuarantorTypeService
    {
        private readonly IGuarantorType _guarantortype;

        
        public GuarantorTypeService(IGuarantorType guarantortype)
        {
            _guarantortype = guarantortype;
        }

        public async Task<List<GuarantorType>> GetAllGuarantorTypeAsync()
        {
            return await _guarantortype.GetAllGuarantorTypeAsync();
        }

        public async Task<GuarantorType?> GetByIdAsync(int id)
        {
            return await _guarantortype.GetByIdAsync(id);
        }

        public async Task CreateGuarantorTypeAsync(CreateGuarantorTypeDTO guarantorTypeDto)
        {
            
            await _guarantortype.CreateGuarantorTypeAsync(guarantorTypeDto);
        }

        public async Task UpdateGuarantorAsync(int id, UpdateGuarantorTypeDTO guarantorTypeDto)
        {
            
            var existing = await _guarantortype.GetByIdAsync(id);
            if (existing == null)
            {
                
                return;
            }

            await _guarantortype.UpdateBorrowerAsync(id, guarantorTypeDto);
        }
    }
}