using Domain.Entities;
using Application.DTO;

namespace Application.Interfaces
{
    public interface IProcessFeeDeposit
    {
        Task<List<ProcessFeeDeposit>> GetAllAsync();
        Task<ProcessFeeDeposit> GetByIdAsync(int id);
        Task CreateProcessFeeDepositAsync(CreateProcessFeeDepositDTO createProcessFeeDepositDTO);
        Task UpdateProcessFeeDepositAsync(UpdateProcessFeeDepositDTO updateProcessFeeDepositDTO);
    }}