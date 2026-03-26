using Application.DTO;
using Application.Interface;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class LoanProductRepository : ILoanProduct
    {
        private readonly ApplicationDbContext _context;

        public LoanProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<LoanProduct>> GetAllAsync()
        {
            return await _context.LoanProducts
                .OrderByDescending(p => p.Id)
                .ToListAsync();
        }

        public async Task<LoanProduct?> GetByIdAsync(int id)
        {
            return await _context.LoanProducts.FindAsync(id);
        }

        public async Task<LoanProduct> CreateAsync(LoanProductDTO dto)
        {
            var product = new LoanProduct
            {
                ProductName = dto.ProductName,
                InterestRate = dto.InterestRate ?? 0,
                Description = dto.Description,
                // We set this internally rather than trusting the DTO
                CreatedAt = DateTime.UtcNow 
            };

            await _context.LoanProducts.AddAsync(product);
            await _context.SaveChangesAsync();
            
            return product;
        }

        public async Task UpdateAsync(int id, LoanProductDTO dto)
        {
            var existingProduct = await _context.LoanProducts.FindAsync(id);
            
            if (existingProduct != null)
            {
                existingProduct.ProductName = dto.ProductName;
                existingProduct.InterestRate = dto.InterestRate ?? existingProduct.InterestRate;
                existingProduct.Description = dto.Description;
                
                // Note: We typically do not update 'CreatedAt'
                
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.LoanProducts.FindAsync(id);
            if (product != null)
            {
                _context.LoanProducts.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(string productName)
        {
            return await _context.LoanProducts
                .AnyAsync(p => p.ProductName.ToLower() == productName.ToLower());
        }
    }
}