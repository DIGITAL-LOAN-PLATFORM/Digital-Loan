using Application.DTO;
using Domain.Entities;
using Application.Interfaces;

namespace Application.Services.ProcessFeeDeposits
{
    public class ProcessFeeDepositService : IProcessFeeDepositService
    {
        private readonly IProcessFeeDeposit _processFeeDeposit;

        public ProcessFeeDepositService(IProcessFeeDeposit processFeeDeposit)
        {
            _processFeeDeposit = processFeeDeposit;
        }

        public async Task<List<ProcessFeeDeposit>> GetAllAsync()
        {
            return await _processFeeDeposit.GetAllAsync();
        }

        public async Task<ProcessFeeDeposit> GetByIdAsync(int id)
        {
            return await _processFeeDeposit.GetByIdAsync(id);
        }

        public async Task CreateProcessFeeDepositAsync(CreateProcessFeeDepositDTO createProcessFeeDepositDTO)
        {
            await _processFeeDeposit.CreateProcessFeeDepositAsync(createProcessFeeDepositDTO);
        }

        public async Task UpdateProcessFeeDepositAsync(UpdateProcessFeeDepositDTO updateProcessFeeDepositDTO)
        {
            await _processFeeDeposit.UpdateProcessFeeDepositAsync(updateProcessFeeDepositDTO);
        }

      
    }
}