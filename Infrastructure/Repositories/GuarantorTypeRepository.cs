using Application.Interface;
using Domain.Entities;
using Infrastructure.Data;
using Application.DTO;
using Microsoft.EntityFrameworkCore; 

namespace Infrastructure.Repository
{
    public class GuarantorTypeRepository : IGuarantorType
    {
        private readonly ApplicationDbContext _dbcontext;

        public GuarantorTypeRepository(ApplicationDbContext context)
        {
            _dbcontext = context;
        }

        public async Task<List<GuarantorType>> GetAllGuarantorTypeAsync()
        {
            
            return await _dbcontext.GuarantorTypes.ToListAsync();
        }

        public async Task<GuarantorType?> GetByIdAsync(int id)
        {
            return await _dbcontext.GuarantorTypes.FindAsync(id);
        }

        public async Task CreateGuarantorTypeAsync(CreateGuarantorTypeDTO guarantorTypeDto)
        {
            
            GuarantorType newGuarantorType = new GuarantorType
            {
                Name = guarantorTypeDto.Name,
            };

            
            await _dbcontext.GuarantorTypes.AddAsync(newGuarantorType);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task UpdateBorrowerAsync(int id, UpdateGuarantorTypeDTO guarantorTypeDto)
        {
            
            var guarantortype = await _dbcontext.GuarantorTypes.FindAsync(id);
            
            if (guarantortype == null) return;

            guarantortype.Name = guarantorTypeDto.Name;
           
            await _dbcontext.SaveChangesAsync();
        }
    }
}