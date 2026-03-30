using Application.DTO;
using Application.Interface;
using Application.Services.LoanRequirements;
using Domain.Entities;

namespace Application.Services
{
    public class LoanRequirementsService : ILoanRequirementsService
    {
        private readonly ILoanRequirements _repository;

        public LoanRequirementsService(ILoanRequirements repository)
        {
            _repository = repository;
        }

        public async Task<List<LoanRequirementsDTO>> GetAllRequirementsAsync()
        {
            var requirements = await _repository.GetAllAsync();
            return requirements.Select(MapToDto).ToList();
        }

        public async Task<LoanRequirementsDTO?> GetRequirementByIdAsync(int id)
        {
            var requirement = await _repository.GetByIdAsync(id);
            return requirement != null ? MapToDto(requirement) : null;
        }

        public async Task<List<LoanRequirementsDTO>> GetRequirementsByProductIdAsync(int productId)
        {
            var requirements = await _repository.GetByProductIdAsync(productId);
            return requirements.Select(MapToDto).ToList();
        }

        public async Task<bool> CreateRequirementAsync(CreateLoanRequirementsDTO dto)
        {
            try
            {
                // Check for duplicate names
                if (await _repository.ExistsByNameAsync(dto.Name))
                {
                    return false;
                }

                await _repository.CreateAsync(dto);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateRequirementAsync(int id, UpdateLoanRequirementsDTO dto)
        {
            try
            {
                // Verify requirement exists
                var existing = await _repository.GetByIdAsync(id);
                if (existing == null)
                {
                    return false;
                }

                await _repository.UpdateAsync(id, dto);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteRequirementAsync(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private LoanRequirementsDTO MapToDto(Domain.Entities.LoanRequirements requirement)
        {
            return new LoanRequirementsDTO
            {
                Id = requirement.Id,
                Name = requirement.Name,
                LoanProductId = requirement.LoanProductId,
                ProductName = requirement.LoanProduct?.ProductName ?? "Unknown",
                IsMandatory = requirement.IsMandatory,
                RequiredDocumentId = requirement.RequiredDocumentId,
                DocumentName = requirement.RequiredDocument?.DocumentName ?? "Unknown",
                CreatedAt = requirement.CreatedAt,
                IsActive = requirement.IsActive
            };
        }
    }
}
