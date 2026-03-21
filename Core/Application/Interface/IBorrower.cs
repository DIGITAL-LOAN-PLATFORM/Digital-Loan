using Domain.Entities;
using Application.DTO;

namespace Application.Interface
{
    public interface IBorrower
    {
        Task<Borrower?> GetByIdentificationNumberAsync(string identificationNumber);
        Task<List<Borrower>> GetAllBorrowersAsync();
        Task<Borrower?> GetByIdAsync(int id);
        Task CreateBorrowerAsync(CreateBorrowerDTO borrowerDto);
        Task UpdateBorrowerAsync(int id, UpdateBorrowerDTO borrowerDto);
    }
}
