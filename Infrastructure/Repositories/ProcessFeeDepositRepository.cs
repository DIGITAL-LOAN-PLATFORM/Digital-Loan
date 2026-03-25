using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.DTO;

namespace Infrastructure.Repositories
{
    public class ProcessFeeDepositRepository : IProcessFeeDeposit
    {
        private readonly ApplicationDbContext _context;

        public ProcessFeeDepositRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<ProcessFeeDeposit>> GetAllAsync()
        {
            return await _context.ProcessFeeDeposits.ToListAsync();
        }
        public async Task<ProcessFeeDeposit> GetByIdAsync(int id)
        {
            var processFeeDeposit = await _context.ProcessFeeDeposits.FindAsync(id);

            return processFeeDeposit ?? throw new KeyNotFoundException($"ProcessFeeDeposit with ID {id} was not found.");
        }
        public async Task CreateProcessFeeDepositAsync(CreateProcessFeeDepositDTO createProcessFeeDepositDTO)
        {
            var processFeeDeposit = new ProcessFeeDeposit
            {
                LoanApplication = createProcessFeeDepositDTO.LoanApplication,
                AmountPaid = createProcessFeeDepositDTO.AmountPaid,
                PaymentType = createProcessFeeDepositDTO.PaymentType,

                Account = createProcessFeeDepositDTO.Account,
                DepositDate = createProcessFeeDepositDTO.DepositDate,
                CustomerAccount = createProcessFeeDepositDTO.CustomerAccount

                
            };

            _context.ProcessFeeDeposits.Add(processFeeDeposit);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProcessFeeDepositAsync(UpdateProcessFeeDepositDTO updateProcessFeeDepositDTO)
        {
            var processFeeDeposit = await _context.ProcessFeeDeposits.FindAsync(updateProcessFeeDepositDTO.LoanApplication);

            if (processFeeDeposit == null)
            {
                throw new KeyNotFoundException($"ProcessFeeDeposit with ID {updateProcessFeeDepositDTO.LoanApplication} was not found.");
            }

            processFeeDeposit.LoanApplication = updateProcessFeeDepositDTO.LoanApplication;
            processFeeDeposit.AmountPaid = updateProcessFeeDepositDTO.AmountPaid;
            processFeeDeposit.PaymentType = updateProcessFeeDepositDTO.PaymentType;

            processFeeDeposit.Account = updateProcessFeeDepositDTO.Account;
            processFeeDeposit.DepositDate = updateProcessFeeDepositDTO.DepositDate;
            processFeeDeposit.CustomerAccount = updateProcessFeeDepositDTO.CustomerAccount;

            _context.ProcessFeeDeposits.Update(processFeeDeposit);
            await _context.SaveChangesAsync();
        }

       
    }
}
