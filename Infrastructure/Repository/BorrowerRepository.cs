using Application.Interface;
using Application.DTO;
using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BorrowerRepository : IBorrower
    {
        private readonly ApplicationDbContext _dbContext;

        public BorrowerRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task<Borrower?> GetByIdentificationNumberAsync(string identificationNumber)
        {
            return await _dbContext.Borrowers
                .FirstOrDefaultAsync(b => b.IdentificationNumber == identificationNumber);
        }

        public async Task<List<Borrower>> GetAllBorrowersAsync()
        {
            return await _dbContext.Borrowers.ToListAsync();
        }

        public async Task<Borrower?> GetByIdAsync(int id)
        {
            return await _dbContext.Borrowers.FindAsync(id);
        }

        public async Task CreateBorrowerAsync(CreateBorrowerDTO borrowerDto)
        {
            var newBorrower = new Borrower
            {
                IdentificationNumber = borrowerDto.IdentificationNumber,
                FirstName = borrowerDto.FirstName,
                LastName = borrowerDto.LastName,
                Gender = borrowerDto.Gender,
                Email = borrowerDto.Email,
                PhoneNumber = borrowerDto.PhoneNumber,
                DOB = borrowerDto.DOB,
                MaritalStatus = borrowerDto.MaritalStatus,
                SpouseName = borrowerDto.SpouseName,
                SpouseNidaNumber = borrowerDto.SpouseNidaNumber,
                HomeLocation = new Location
                {
                    Province = borrowerDto.Location.Province,
                    District = borrowerDto.Location.District,
                    Sector = borrowerDto.Location.Sector,
                    Cell = borrowerDto.Location.Cell,
                    Village = borrowerDto.Location.Village
                }
            };

            await _dbContext.Borrowers.AddAsync(newBorrower);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateBorrowerAsync(int id, UpdateBorrowerDTO borrowerDto)
        {
            var existingBorrower = await _dbContext.Borrowers.FindAsync(id);
            if (existingBorrower == null) return;

            existingBorrower.IdentificationNumber = borrowerDto.IdentificationNumber;
            existingBorrower.FirstName = borrowerDto.FirstName;
            existingBorrower.LastName = borrowerDto.LastName;
            existingBorrower.Gender = borrowerDto.Gender;
            existingBorrower.Email = borrowerDto.Email;
            existingBorrower.PhoneNumber = borrowerDto.PhoneNumber;
            existingBorrower.DOB = borrowerDto.DOB;
            existingBorrower.MaritalStatus = borrowerDto.MaritalStatus;
            existingBorrower.SpouseName = borrowerDto.SpouseName;
            existingBorrower.SpouseNidaNumber = borrowerDto.SpouseNidaNumber;
            existingBorrower.HomeLocation = new Location
            {
                Province = borrowerDto.Location.Province,
                District = borrowerDto.Location.District,
                Sector = borrowerDto.Location.Sector,
                Cell = borrowerDto.Location.Cell,
                Village = borrowerDto.Location.Village
            };

            await _dbContext.SaveChangesAsync();
        }
    }
}
