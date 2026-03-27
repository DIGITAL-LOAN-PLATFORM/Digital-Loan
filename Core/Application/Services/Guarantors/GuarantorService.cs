using Application.DTO;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class GuarantorService : IGuarantorService
    {
        private readonly IGuarantor _repository;
        private readonly ILoanApplication _loanRepository;

        public GuarantorService(IGuarantor repository, ILoanApplication loanRepository)
        {
            _repository = repository;
            _loanRepository = loanRepository;
        }

        // --- CREATE ---
        public async Task<bool> AddGuarantorAsync(CreateGuarantorDTO dto)
        {
            // Business Logic: Check loan status
            var loan = await _loanRepository.GetByIdAsync(dto.LoanApplicationId);
            if (loan == null || loan.Status != "Pending") return false;

            // Business Logic: Check for duplicates
            if (await _repository.IsAlreadyGuarantorAsync(dto.LoanApplicationId, dto.IdentificationNumber))
                return false;

            // Mapping DTO -> Entity
            var entity = new Guarantor
            {
                Name = dto.Name,
                IdentificationNumber = dto.IdentificationNumber,
                Phone = dto.Phone,
                Email = dto.Email,
                DOB = dto.DOB,
                ResidentialAddress = dto.ResidentialAddress,
                LoanApplicationId = dto.LoanApplicationId,
                GuarantorTypeId = dto.GuarantorTypeId
            };

            await _repository.AddAsync(entity);
            return true;
        }

        // --- UPDATE ---
        public async Task<bool> UpdateGuarantorAsync(int id, UpdateGuarantorDTO dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            // Mapping DTO -> Existing Entity
            entity.Name = dto.Name;
            entity.Phone = dto.Phone;
            entity.Email = dto.Email;
            entity.DOB = dto.DOB;
            entity.ResidentialAddress = dto.ResidentialAddress;
            entity.GuarantorTypeId = dto.GuarantorTypeId;

            await _repository.UpdateAsync(entity);
            return true;
        }

        // --- READ ---
        public async Task<IEnumerable<GuarantorDTO>> GetGuarantorsByLoanAsync(int loanApplicationId)
        {
            var guarantors = await _repository.GetByLoanApplicationIdAsync(loanApplicationId);
            
            return guarantors.Select(g => new GuarantorDTO
            {
                Id = g.Id,
                Name = g.Name,
                IdentificationNumber = g.IdentificationNumber,
                Phone = g.Phone,
                Email = g.Email,
                DOB = g.DOB,
                ResidentialAddress = g.ResidentialAddress,
                LoanApplicationId = g.LoanApplicationId,
                GuarantorTypeId = g.GuarantorTypeId,
                GuarantorTypeName = g.GuarantorType?.Name 
            });
        }

        public async Task<GuarantorDTO?> GetGuarantorByIdAsync(int id)
        {
            var g = await _repository.GetByIdAsync(id);
            if (g == null) return null;

            return new GuarantorDTO
            {
                Id = g.Id,
                Name = g.Name,
                IdentificationNumber = g.IdentificationNumber,
                Phone = g.Phone,
                Email = g.Email,
                DOB = g.DOB,
                ResidentialAddress = g.ResidentialAddress,
                LoanApplicationId = g.LoanApplicationId,
                GuarantorTypeId = g.GuarantorTypeId
            };
        }

        // --- DELETE ---
        public async Task<bool> RemoveGuarantorAsync(int id)
        {
            var guarantor = await _repository.GetByIdAsync(id);
            if (guarantor == null) return false;

            var loan = await _loanRepository.GetByIdAsync(guarantor.LoanApplicationId);
            if (loan?.Status != "Pending") return false;

            await _repository.DeleteAsync(id);
            return true;
        }
    }
}