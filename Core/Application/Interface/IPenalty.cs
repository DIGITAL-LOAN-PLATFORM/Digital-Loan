using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IPenalty
    {
        Task<Penalty?> GetByIdAsync(int id);
        Task<IEnumerable<Penalty>> GetByLoanIdAsync(int loanDisbursementId);
        Task<IEnumerable<Penalty>> GetUnpaidPenaltiesByLoanIdAsync(int loanDisbursementId);
        Task<bool> AddAsync(Penalty penalty);
        Task<bool> UpdateAsync(Penalty penalty);
        Task<bool> PenaltyExistsForDateAsync(int loanDisbursementId, DateTime eventDate);

        // FIX: Change DTO to Entity here
        Task<IEnumerable<RepaymentScheduleItem>> GetNewlyOverdueInstallmentsAsync();
    }
}