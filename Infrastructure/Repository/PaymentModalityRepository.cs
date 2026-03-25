using Application.Interface;
using Domain.Entities;
using Infrastructure.Data;
using Application.DTO;
using Microsoft.EntityFrameworkCore; 

namespace Infrastructure.Repository
{
    public class PaymentModalityRepository : IPaymentModality
    {
        private readonly ApplicationDbContext _dbcontext;

        public PaymentModalityRepository(ApplicationDbContext context)
        {
            _dbcontext = context;
        }

        // 1. Matched name to Interface
        public async Task<List<PaymentModality>> GetAllPaymentModalitiesAsync()
        {
            return await _dbcontext.PaymentModalities.ToListAsync();
        }

        public async Task<PaymentModality?> GetByIdAsync(int id)
        {
            return await _dbcontext.PaymentModalities.FindAsync(id);
        }

        // 2. Matched name to Interface and fixed entity type
        public async Task CreatePaymentModalityAsync(CreatePaymentModalityDTO dto)
        {
            var newModality = new PaymentModality
            {
                Modality = dto.Modality,
            };

            await _dbcontext.PaymentModalities.AddAsync(newModality);
            await _dbcontext.SaveChangesAsync();
        }

        // 3. Matched name to Interface
        public async Task UpdatePaymentModalityAsync(int id, UpdatePaymentModalityDTO dto)
        {
            var modality = await _dbcontext.PaymentModalities.FindAsync(id);
            
            if (modality == null) return;

            modality.Modality = dto.Modality;
           
            await _dbcontext.SaveChangesAsync();
        }
    }
}