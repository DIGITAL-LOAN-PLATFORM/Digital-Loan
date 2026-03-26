using Application.DTO;
using Application.Interface;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BorrowerRepository(ApplicationDbContext context) : IBorrower
{
    private readonly ApplicationDbContext _context = context;

    public async Task<List<Borrower>> GetAllBorrowersAsync()
    {
        return await _context.Borrowers
            .Where(b => b.IsActive)
            .ToListAsync();
    }

    // renamed from GetBorrowerByIdAsync → GetByIdAsync to match interface
    public async Task<Borrower?> GetByIdAsync(int id)
    {
        return await _context.Borrowers.FindAsync(id);
    }

    // new method — was missing from repository
    public async Task<Borrower?> GetByIdentificationNumberAsync(string identificationNumber)
    {
        return await _context.Borrowers
            .FirstOrDefaultAsync(b => b.IdentificationNumber == identificationNumber && b.IsActive);
    }

    // return type changed from Task<Borrower> → Task<Borrower> (kept, interface updated to match)
    public async Task<Borrower> CreateBorrowerAsync(CreateBorrowerDTO dto)
    {
        var borrower = new Borrower
        {
            IdentificationNumber = dto.IdentificationNumber,
            FirstName            = dto.FirstName,
            LastName             = dto.LastName,
            Gender               = dto.Gender,
            Email                = dto.Email,
            PhoneNumber          = dto.PhoneNumber,
            DOB                  = dto.DOB,
            MaritalStatus        = dto.MaritalStatus,
            SpouseName           = dto.SpouseName,
            SpouseNidaNumber     = dto.SpouseNidaNumber,
            Province             = dto.Province,
            District             = dto.District,
            Sector               = dto.Sector,
            Cell                 = dto.Cell,
            Village              = dto.Village,
        };

        _context.Borrowers.Add(borrower);
        await _context.SaveChangesAsync();
        return borrower;
    }

    // return type kept as Task<bool> (interface updated to match)
    public async Task<bool> UpdateBorrowerAsync(int id, UpdateBorrowerDTO dto)
    {
        var borrower = await _context.Borrowers.FindAsync(id);
        if (borrower is null) return false;

        borrower.IdentificationNumber = dto.IdentificationNumber;
        borrower.FirstName            = dto.FirstName;
        borrower.LastName             = dto.LastName;
        borrower.Gender               = dto.Gender;
        borrower.Email                = dto.Email;
        borrower.PhoneNumber          = dto.PhoneNumber;
        borrower.DOB                  = dto.DOB;
        borrower.MaritalStatus        = dto.MaritalStatus;
        borrower.SpouseName           = dto.SpouseName;
        borrower.SpouseNidaNumber     = dto.SpouseNidaNumber;
        borrower.Province             = dto.Province;
        borrower.District             = dto.District;
        borrower.Sector               = dto.Sector;
        borrower.Cell                 = dto.Cell;
        borrower.Village              = dto.Village;
        borrower.UpdatedAt            = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteBorrowerAsync(int id)
    {
        var borrower = await _context.Borrowers.FindAsync(id);
        if (borrower is null) return false;

        borrower.IsActive  = false;
        borrower.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }
}