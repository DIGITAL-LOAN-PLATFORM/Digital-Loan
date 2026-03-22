using Application.DTO;
using Application.Interface;
using Domain.Entities;

namespace Application.Services
{
    public class LoanProductService : ILoanProductService
    {
        private readonly ILoanProduct _loanproduct;

        public LoanProductService(ILoanProduct loanproduct)
        {
            _loanproduct = loanproduct;
        }

        public async Task<List<LoanProduct>> GetAllLoanProductsAsync()
        {
            return await _loanproduct.GetAllAsync();
        }

        public async Task<LoanProduct?> GetProductByIdAsync(int id)
        {
            return await _loanproduct.GetByIdAsync(id);
        }

        public async Task<bool> CreateProductAsync(LoanProductDTO dto)
        {
            // Business Logic: Prevent duplicate product names
            if (await _loanproduct.ExistsAsync(dto.ProductName!))
            {
                return false; // Or throw a custom exception
            }

            await _loanproduct.CreateAsync(dto);
            return true;
        }

        public async Task<bool> UpdateProductAsync(int id, LoanProductDTO dto)
        {
            var existing = await _loanproduct.GetByIdAsync(id);
            if (existing == null) return false;

            // Check if name is being changed to something that already exists elsewhere
            if (existing.ProductName != dto.ProductName && await _loanproduct.ExistsAsync(dto.ProductName!))
            {
                return false;
            }

            await _loanproduct.UpdateAsync(id, dto);
            return true;
        }

        public async Task DeleteProductAsync(int id)
        {
            await _loanproduct.DeleteAsync(id);
        }
    }
}