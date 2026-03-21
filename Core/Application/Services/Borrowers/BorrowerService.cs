using Domain.Entities;
using Application.Interface;
using Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Borrowers
{
    public class BorrowerService : IBorrowerService
    {
        private readonly IBorrower borrowerRepository;

        public BorrowerService(IBorrower borrowerRepository)
        {
            this.borrowerRepository = borrowerRepository;
        }

        public async Task<Borrower?> GetByIdentificationNumberAsync(string identificationNumber)
        {
            return await borrowerRepository.GetByIdentificationNumberAsync(identificationNumber);
        }

        public async Task<List<Borrower>> GetAllBorrowersAsync()
        {
            return await borrowerRepository.GetAllBorrowersAsync();
        }

        public async Task<Borrower?> GetByIdAsync(int id)
        {
            return await borrowerRepository.GetByIdAsync(id);
        }

        public async Task CreateBorrowerAsync(CreateBorrowerDTO borrowerDto)
        {
            await borrowerRepository.CreateBorrowerAsync(borrowerDto);
        }

        public async Task UpdateBorrowerAsync(int id, UpdateBorrowerDTO borrowerDto)
        {
            await borrowerRepository.UpdateBorrowerAsync(id, borrowerDto);
        }
    }
}
