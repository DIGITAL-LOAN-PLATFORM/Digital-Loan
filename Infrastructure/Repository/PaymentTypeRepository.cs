using Application.Interface;
using Application.DTO;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PaymentTypeRepository : IPaymentType
    {
        private readonly ApplicationDbContext _dbContext;

        public PaymentTypeRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task<PaymentType?> GetByIdAsync(int id)
        {
            return await _dbContext.PaymentTypes.FindAsync(id);
        }

        public async Task<List<PaymentType>> GetAllPaymentTypesAsync()
        {
            return await _dbContext.PaymentTypes.ToListAsync();
        }

        public async Task CreatePaymentTypeAsync(CreatePaymentTypeDTO paymentTypeDto)
        {
            var newPaymentType = new PaymentType
            {
                Name = paymentTypeDto.Name
            };

            await _dbContext.PaymentTypes.AddAsync(newPaymentType);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePaymentTypeAsync(int id, UpdatePaymentTypeDTO paymentTypeDto)
        {
            var existingPaymentType = await _dbContext.PaymentTypes.FindAsync(id);
            if (existingPaymentType == null) return;

            existingPaymentType.Name = paymentTypeDto.Name;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePaymentTypeAsync(int id)
        {
            var paymentType = await _dbContext.PaymentTypes.FindAsync(id);
            if (paymentType == null) return;

            _dbContext.PaymentTypes.Remove(paymentType);
            await _dbContext.SaveChangesAsync();
        }
    }
}