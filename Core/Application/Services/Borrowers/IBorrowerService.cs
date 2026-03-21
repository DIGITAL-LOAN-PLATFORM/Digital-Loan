using Domain.Entities;
using Application.DTO; // Ensure this matches your DTO namespace

namespace Application.Services
{
    public interface IBorrowerService
    {
        Task<Borrower?> GetByIdentificationNumberAsync(string identificationNumber);
        
Task<List<Borrower>> GetAllBorrowersAsync();

        Task<Borrower?> GetByIdAsync(int id);

       Task CreateBorrowerAsync(CreateBorrowerDTO borrowerDto);

        Task UpdateBorrowerAsync(int id, UpdateBorrowerDTO borrowerDto);
    }
}