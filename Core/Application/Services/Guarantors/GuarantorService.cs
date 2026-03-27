using Application.DTO;
using Application.Interface;
using Domain.Entities;
using System.Linq;

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

        public async Task<Result<GuarantorDTO>> AddGuarantorAsync(CreateGuarantorDTO dto)
        {
            try
            {
                var loan = await _loanRepository.GetByIdAsync(dto.LoanApplicationId);
                if (loan == null) return Result<GuarantorDTO>.Failure("Loan application not found.");
                if (loan.Status != "Pending") return Result<GuarantorDTO>.Failure("Guarantors can only be added to pending applications.");

                if (await _repository.IsAlreadyGuarantorAsync(dto.LoanApplicationId, dto.IdentificationNumber))
                    return Result<GuarantorDTO>.Failure("Guarantor with this ID number already exists for this loan.");

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

                var savedEntity = await _repository.AddAsync(entity);
                var saved = await _repository.GetByIdAsync(savedEntity.Id);
                return Result<GuarantorDTO>.Success(MapToDto(saved!));
            }
            catch (Exception ex)
            {
                return Result<GuarantorDTO>.Failure($"Unexpected error: {ex.Message}");
            }
        }

        public async Task<Result<GuarantorDTO>> UpdateGuarantorAsync(int id, UpdateGuarantorDTO dto)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null) return Result<GuarantorDTO>.Failure("Guarantor not found.");

                entity.Name = dto.Name;
                entity.Phone = dto.Phone;
                entity.Email = dto.Email;
                entity.DOB = dto.DOB;
                entity.ResidentialAddress = dto.ResidentialAddress;
                entity.GuarantorTypeId = dto.GuarantorTypeId;

                await _repository.UpdateAsync(entity);
                var updated = await _repository.GetByIdAsync(id);
                return Result<GuarantorDTO>.Success(MapToDto(updated!));
            }
            catch (Exception ex)
            {
                return Result<GuarantorDTO>.Failure($"Unexpected error: {ex.Message}");
            }
        }

        public async Task<Result<bool>> RemoveGuarantorAsync(int id)
        {
            try
            {
                var guarantor = await _repository.GetByIdAsync(id);
                if (guarantor == null) return Result<bool>.Failure("Guarantor not found.");

                var loan = await _loanRepository.GetByIdAsync(guarantor.LoanApplicationId);
                if (loan?.Status != "Pending") return Result<bool>.Failure("Cannot remove guarantor from non-pending loan.");

                await _repository.DeleteAsync(id);
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error removing guarantor: {ex.Message}");
            }
        }

        public async Task<Result<bool>> DeleteGuarantorAsync(int id)
        {
            return await RemoveGuarantorAsync(id);
        }

        public async Task<IEnumerable<GuarantorDTO>> GetGuarantorsByLoanAsync(int loanApplicationId)
        {
            var guarantors = await _repository.GetByLoanApplicationIdAsync(loanApplicationId);
            return guarantors.Select(MapToDto);
        }

        public async Task<GuarantorDTO?> GetGuarantorByIdAsync(int id)
        {
            var g = await _repository.GetByIdAsync(id);
            return g != null ? MapToDto(g) : null;
        }

        public async Task<IEnumerable<GuarantorDTO>> GetAllGuarantorsAsync()
        {
            var guarantors = await _repository.GetAllAsync() ?? Enumerable.Empty<Guarantor>();
            return guarantors.Select(MapToDto);
        }

        private GuarantorDTO MapToDto(Guarantor g)
        {
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
                GuarantorTypeId = g.GuarantorTypeId,
                GuarantorTypeName = g.GuarantorType?.Name
            };
        }
    }
}

