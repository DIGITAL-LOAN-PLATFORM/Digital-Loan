using Application.DTO;
using Application.Interface;
using Application.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class LoanDisbursementService : ILoanDisbursementService
    {
        private readonly ILoanDisbursement _repository;
        private readonly ILoanApplication _loanAppRepository;
        private readonly IAccount _accountRepository;

        public LoanDisbursementService(
            ILoanDisbursement repository, 
            ILoanApplication loanAppRepository,
            IAccount accountRepository)
        {
            _repository = repository;
            _loanAppRepository = loanAppRepository;
            _accountRepository = accountRepository;
        }

        public async Task<bool> DisburseLoanAsync(CreateLoanDisbursementDTO dto)
        {
            // 1. Validate application exists and is currently "Approved"
            var loanApp = await _loanAppRepository.GetByIdAsync(dto.LoanApplicationId);
            
            if (loanApp == null || loanApp.Status != "Approved") 
                return false;

            // 1.5 Validate account exists and has sufficient balance
            var account = await _accountRepository.GetByIdAsync(dto.AccountId);
            if (account == null || (account.Balance ?? 0) < dto.PrincipalDisbursed)
                return false; // Account not found or insufficient balance

            // 2. AUTOMATED FINANCIAL CALCULATION
            int gracePeriodDays = 5; 
            DateTime disbursementDate = dto.DisbursementDate; 
            DateTime interestClockStart = disbursementDate.AddDays(gracePeriodDays);
            DateTime calculatedMaturity = interestClockStart.AddMonths(loanApp.LoanTerm);

            // Use the interest rate from the DTO (user can override product rate)
            // Default to product rate if user didn't specify
            decimal interestRate = dto.InterestRate > 0 ? dto.InterestRate : (loanApp.loanProduct?.InterestRate ?? 0);

            // Calculate Monthly Interest (not total upfront)
            decimal monthlyPrincipal = dto.PrincipalDisbursed / loanApp.LoanTerm;
            decimal monthlyInterest = (dto.PrincipalDisbursed * (interestRate / 100)) / 12;

            // 3. Create Entity (The Ledger Entry)
            var disbursement = new LoanDisbursement
            {
                LoanApplicationId = dto.LoanApplicationId,
                PaymentModalityId = loanApp.paymentModalityId, 
                DurationInMonths = loanApp.LoanTerm,
                PrincipalDisbursed = dto.PrincipalDisbursed,
                
                // FIXED: Using new property names for Waterfall logic
                PrincipalBalance = dto.PrincipalDisbursed, 
                InterestBalance = Math.Round(monthlyInterest * loanApp.LoanTerm, 2), // For reference only
                
                // Use the interest rate from the loan product, not user input
                InterestRate = interestRate,
                PaymentMode = dto.PaymentMode,
                ReferenceNumber = dto.ReferenceNumber,
                DisbursementDate = disbursementDate,
                InterestClockStartDate = interestClockStart,
                MaturityDate = calculatedMaturity,
                Status = LoanStatus.Active,
                DisbursedBy = dto.DisbursedBy,
                CreatedAt = DateTime.UtcNow
            };

            // Add repayment schedule items
            for (int i = 1; i <= loanApp.LoanTerm; i++)
            {
                disbursement.RepaymentSchedules.Add(new RepaymentScheduleItem
                {
                    InstallmentNumber = i,
                    DueDate = interestClockStart.AddMonths(i),
                    PrincipalAmount = Math.Round(monthlyPrincipal, 2),
                    InterestAmount = Math.Round(monthlyInterest, 2),
                    TotalAmount = Math.Round(monthlyPrincipal + monthlyInterest, 2),
                    IsPaid = false
                });
            }

            // 4. PERSISTENCE & STATUS TRANSITION
            var result = await _repository.AddAsync(disbursement);

            if (result)
            {
                loanApp.Status = "Disbursed"; 
                await _loanAppRepository.UpdateAsync(loanApp);

                // 5. DEDUCT AMOUNT FROM ACCOUNT
                account.Balance = (account.Balance ?? 0) - dto.PrincipalDisbursed;
                var updateAccountDto = new UpdateAccountDTO
                {
                    Id = account.Id,
                    Name = account.Name ?? string.Empty,
                    Provider = account.Provider ?? string.Empty,
                    Number = account.Number ?? string.Empty,
                    Type = account.Type ?? 0,
                    Balance = account.Balance.Value
                };
                await _accountRepository.UpdateAccountAsync(updateAccountDto);
            }

            return result;
        }

        public async Task<LoanDisbursementDTO?> GetDisbursementByIdAsync(int id)
        {
            var d = await _repository.GetByIdAsync(id);
            if (d == null) return null;

            var dto = MapToDto(d);
            dto.RepaymentSchedule = GenerateAmortizationSchedule(d);
            
            return dto;
        }

        private List<RepaymentScheduleItemDTO> GenerateAmortizationSchedule(LoanDisbursement d)
        {
            // IMPORTANT: This method always calculates the schedule using the actual disbursed interest rate
            // stored in d.InterestRate, ensuring the repayment schedule matches the rate at which the 
            // loan was originally disbursed, not the current product rate
            var schedule = new List<RepaymentScheduleItemDTO>();
            
            decimal monthlyPrincipal = d.PrincipalDisbursed / d.DurationInMonths;
            // Use the actual disbursed interest rate to calculate monthly interest
            decimal monthlyInterest = (d.PrincipalDisbursed * (d.InterestRate / 100)) / 12;

            for (int i = 1; i <= d.DurationInMonths; i++)
            {
                schedule.Add(new RepaymentScheduleItemDTO
                {
                    InstallmentNumber = i,
                    DueDate = d.InterestClockStartDate.AddMonths(i),
                    PrincipalAmount = Math.Round(monthlyPrincipal, 2),
                    InterestAmount = Math.Round(monthlyInterest, 2),
                    TotalAmount = Math.Round(monthlyPrincipal + monthlyInterest, 2)
                });
            }

            return schedule;
        }

        public async Task<IEnumerable<LoanDisbursementDTO>> GetAllDisbursementsAsync()
        {
            var data = await _repository.GetAllAsync();
            return data.Select(MapToDto);
        }

        public async Task<IEnumerable<LoanDisbursementDTO>> GetDisbursementsByLoanIdAsync(int loanApplicationId)
        {
            var data = await _repository.GetByLoanApplicationIdAsync(loanApplicationId);
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
                BorrowerName = d.LoanApplication?.Borrower != null 
                    ? $"{d.LoanApplication.Borrower.FirstName} {d.LoanApplication.Borrower.LastName}" 
                    : "Unknown",
                PrincipalDisbursed = d.PrincipalDisbursed,
                
                // FIXED: Mapping new property names to the DTO
                CurrentPrincipalBalance = d.PrincipalBalance,
                TotalInterestAccrued = d.InterestBalance,
                
                TotalPenaltiesAccrued = d.TotalPenaltiesAccrued,
                InterestRate = d.InterestRate,
                PaymentMode = d.PaymentMode,
                ReferenceNumber = d.ReferenceNumber,
                DisbursementDate = d.DisbursementDate,
                InterestClockStartDate = d.InterestClockStartDate,
                MaturityDate = d.MaturityDate,
                StatusName = d.Status.ToString(),
                Status = d.Status,
                CreatedAt = d.CreatedAt
            };
        }
    }
}