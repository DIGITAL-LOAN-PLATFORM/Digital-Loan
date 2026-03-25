using Application.DTO;
using Domain.Entities;

namespace Application.Services
{
    public interface ILoanProductService
    {
        Task<List<LoanProduct>> GetAllLoanProductsAsync();
        Task<LoanProduct?> GetProductByIdAsync(int id);
        Task<bool> CreateProductAsync(LoanProductDTO dto);
        Task<bool> UpdateProductAsync(int id, LoanProductDTO dto);
        Task DeleteProductAsync(int id);
    }
}