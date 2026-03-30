using Application.DTO;
using Application.Interface;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class LoanRequirementsRepository : ILoanRequirements
    {
        private readonly ApplicationDbContext _context;

        public LoanRequirementsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<LoanRequirements>> GetAllAsync()
        {
            return await _context.LoanRequirements
                .Include(lr => lr.LoanProduct)
                .Include(lr => lr.RequiredDocument)
                .OrderByDescending(lr => lr.Id)
                .ToListAsync();
        }

        public async Task<LoanRequirements?> GetByIdAsync(int id)
        {
            return await _context.LoanRequirements
                .Include(lr => lr.LoanProduct)
                .Include(lr => lr.RequiredDocument)
                .FirstOrDefaultAsync(lr => lr.Id == id);
        }

        public async Task<List<LoanRequirements>> GetByProductIdAsync(int productId)
        {
            return await _context.LoanRequirements
                .Include(lr => lr.LoanProduct)
                .Include(lr => lr.RequiredDocument)
                .Where(lr => lr.LoanProductId == productId && lr.IsActive)
                .OrderBy(lr => lr.Name)
                .ToListAsync();
        }

        public async Task<LoanRequirements> CreateAsync(CreateLoanRequirementsDTO dto)
        {
            var requirement = new LoanRequirements
            {
                Name = dto.Name,
                LoanProductId = dto.LoanProductId,
                IsMandatory = dto.IsMandatory,
                RequiredDocumentId = dto.RequiredDocumentId,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _context.LoanRequirements.AddAsync(requirement);
            await _context.SaveChangesAsync();

            return requirement;
        }

        public async Task UpdateAsync(int id, UpdateLoanRequirementsDTO dto)
        {
            var requirement = await _context.LoanRequirements.FindAsync(id);
            if (requirement != null)
            {
                requirement.Name = dto.Name;
                requirement.LoanProductId = dto.LoanProductId;
                requirement.IsMandatory = dto.IsMandatory;
                requirement.RequiredDocumentId = dto.RequiredDocumentId;
                requirement.IsActive = dto.IsActive;

                _context.LoanRequirements.Update(requirement);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var requirement = await _context.LoanRequirements.FindAsync(id);
            if (requirement != null)
            {
                _context.LoanRequirements.Remove(requirement);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.LoanRequirements
                .AnyAsync(lr => lr.Name.ToLower() == name.ToLower());
        }
    }
}
