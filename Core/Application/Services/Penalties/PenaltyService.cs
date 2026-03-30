using Application.DTO;

using Application.Interface;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interface; // Ensure this is where IPenalty and ILoanDisbursement live
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PenaltyService : IPenaltyService
    {
        private readonly IPenalty _penaltyRepo;
        private readonly ILoanDisbursement _loanRepo;

        public PenaltyService(IPenalty penaltyRepo, ILoanDisbursement loanRepo)
        {
            _penaltyRepo = penaltyRepo;
            _loanRepo = loanRepo;
        }

        // 1. The "Day 1" 5% Penalty Engine
        public async Task<int> ApplyDayOnePenaltiesAsync()
        {
            int count = 0;
            var overdueItems = await _loanRepo.GetNewlyOverdueInstallmentsAsync();

            foreach (var item in overdueItems)
            {
                decimal penaltyAmount = Math.Round(item.TotalAmount * 0.05m, 2);

                var penalty = new Penalty
                {
                    LoanDisbursementId = item.LoanDisbursementId,
                    RepaymentScheduleItemId = item.Id,
                    PenaltyAmount = penaltyAmount,
                    CurrentBalance = penaltyAmount,
                    ReasonId = 1, 
                    EventDate = DateTime.Today,
                    Status = PenaltyStatus.Active,
                    IsConfirmed = true,
                    Remarks = $"5% Penalty applied: 1 day late for Installment #{item.InstallmentNumber}",
                    CreatedAt = DateTime.UtcNow
                };

                bool saved = await _penaltyRepo.AddAsync(penalty);
                if (saved)
                {
                    item.LastPenaltyDate = DateTime.Today;
                    await _loanRepo.UpdateScheduleItemAsync(item);
                    count++;
                }
            }
            return count;
        }

        // 2. Implementation of Missing Interface Members
        public async Task<PenaltyDTO?> GetPenaltyByIdAsync(int id)
        {
            var p = await _penaltyRepo.GetByIdAsync(id);
            return p == null ? null : MapToDto(p);
        }

        public async Task<IEnumerable<PenaltyDTO>> GetPenaltiesByLoanIdAsync(int loanDisbursementId)
        {
            var penalties = await _penaltyRepo.GetByLoanIdAsync(loanDisbursementId);
            return penalties.Select(MapToDto);
        }

        public async Task<bool> WaivePenaltyAsync(int penaltyId, string reason, string managerUserId)
        {
            var penalty = await _penaltyRepo.GetByIdAsync(penaltyId);
            if (penalty == null) return false;

            penalty.Status = PenaltyStatus.Waived;
            penalty.Remarks += $" | Waived by {managerUserId}: {reason}";
            return await _penaltyRepo.UpdateAsync(penalty);
        }

        public async Task<bool> ConfirmPenaltyAsync(int penaltyId, string managerUserId)
        {
            var penalty = await _penaltyRepo.GetByIdAsync(penaltyId);
            if (penalty == null) return false;

            penalty.Status = PenaltyStatus.Active;
            penalty.IsConfirmed = true;
            penalty.ConfirmedByUserId = managerUserId;
            penalty.ConfirmedAt = DateTime.UtcNow;
            return await _penaltyRepo.UpdateAsync(penalty);
        }

        // Helper for DTO mapping
        private PenaltyDTO MapToDto(Penalty p)
        {
            return new PenaltyDTO
            {
                Id = p.Id,
                PenaltyAmount = p.PenaltyAmount,
                CurrentBalance = p.CurrentBalance,
                Status = p.Status.ToString(),
                EventDate = p.EventDate,
                Remarks = p.Remarks
            };
        }
    }
}