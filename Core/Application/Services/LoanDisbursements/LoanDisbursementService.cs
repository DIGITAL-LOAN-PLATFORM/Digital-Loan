using Application.DTO;
using Application.Interface;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Services
{
    public class LoanDisbursementService : ILoanDisbursementService
    {
        private readonly ILoanDisbursement _repository;
        private readonly ILoanApplication _loanAppRepository;

        public LoanDisbursementService(
            ILoanDisbursement repository, 
            ILoanApplication loanAppRepository)
        {
            _repository = repository;
            _loanAppRepository = loanAppRepository;
        }

        public async Task<bool> DisburseLoanAsync(CreateLoanDisbursementDTO dto)
        {
            // 1. Validate application exists
            var loanApp = await _loanAppRepository.GetByIdAsync(dto.LoanApplicationId);
            if (loanApp == null) return false;

            // 2. Create Entity (Only map IDs to avoid EF tracking conflicts)
            var disbursement = new LoanDisbursement
            {
                LoanApplicationId = dto.LoanApplicationId,
                PrincipalDisbursed = dto.PrincipalDisbursed,
                CurrentPrincipalBalance = dto.PrincipalDisbursed, // Initial balance
                InterestRate = dto.InterestRate,
                PaymentMode = dto.PaymentMode,
                ReferenceNumber = dto.ReferenceNumber,
                DisbursementDate = dto.DisbursementDate,
                InterestClockStartDate = dto.InterestClockStartDate,
                MaturityDate = dto.MaturityDate,
                Status = LoanStatus.Active,
                DisbursedBy = dto.DisbursedBy,
                CreatedAt = DateTime.UtcNow
            };

            return await _repository.AddAsync(disbursement);
        }

        public async Task<LoanDisbursementDTO?> GetDisbursementByIdAsync(int id)
        {
            var d = await _repository.GetByIdAsync(id);
            return d == null ? null : MapToDto(d);
        }

        public async Task<IEnumerable<LoanDisbursementDTO>> GetAllDisbursementsAsync()
        {
            var data = await _repository.GetAllAsync();
            return data.Select(MapToDto);
        }

        public async Task<bool> UpdateDisbursementAsync(int id, UpdateLoanDisbursementDTO dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            if (!string.IsNullOrEmpty(dto.ReferenceNumber))
                existing.ReferenceNumber = dto.ReferenceNumber;

            existing.Status = (LoanStatus)dto.Status;

            return await _repository.UpdateAsync(existing);
        }

        private LoanDisbursementDTO MapToDto(LoanDisbursement d)
        {
            return new LoanDisbursementDTO
            {
                Id = d.Id,
                LoanApplicationId = d.LoanApplicationId,
                ApplicationNumber = d.LoanApplication?.ApplicationNumber ?? "N/A",
                
                // Pull Modality from Application since it was removed from Disbursement
                PaymentModality = d.LoanApplication?.paymentModality,
                
                BorrowerName = d.LoanApplication?.Borrower != null 
                    ? $"{d.LoanApplication.Borrower.FirstName} {d.LoanApplication.Borrower.LastName}".Trim() 
                    : "Unknown",
                
                PrincipalDisbursed = d.PrincipalDisbursed,
                CurrentPrincipalBalance = d.CurrentPrincipalBalance,
                TotalInterestAccrued = d.TotalInterestAccrued,
                TotalPenaltiesAccrued = d.TotalPenaltiesAccrued,
                InterestRate = d.InterestRate,
                PaymentMode = d.PaymentMode,
                ReferenceNumber = d.ReferenceNumber,
                DisbursementDate = d.DisbursementDate,
                MaturityDate = d.MaturityDate,
                Status = d.Status,
                StatusName = d.Status.ToString(),
                CreatedAt = d.CreatedAt
            };
        }

        public async Task<IEnumerable<LoanDisbursementDTO>> GetDisbursementsByLoanIdAsync(int loanApplicationId)
        {
            var data = await _repository.GetByLoanApplicationIdAsync(loanApplicationId);
            return data.Select(MapToDto);
        }
    }
}