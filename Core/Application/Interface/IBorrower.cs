using Domain.Entities;
using Application.DTO;

namespace Application.Interface
{
    public interface IBorrower
    {
        Task<List<Borrower>> GetAllBorrowersAsync();
        Task<Borrower?> GetByIdAsync(int id);
        Task<Borrower?> GetByIdentificationNumberAsync(string identificationNumber);
        Task<Borrower> CreateBorrowerAsync(CreateBorrowerDTO dto);
        Task<bool> UpdateBorrowerAsync(int id, UpdateBorrowerDTO dto);
        Task<bool> DeleteBorrowerAsync(int id);
    }
}