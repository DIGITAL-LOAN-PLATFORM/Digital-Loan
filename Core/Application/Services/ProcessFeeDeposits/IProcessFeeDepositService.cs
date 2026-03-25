using Application.DTO;
using Domain.Entities;
using Application.Interfaces;

namespace Application.Services.ProcessFeeDeposits   
{
    public interface IProcessFeeDepositService
    {
        Task<List<ProcessFeeDeposit>> GetAllAsync();
        Task<ProcessFeeDeposit> GetByIdAsync(int id);
        Task CreateProcessFeeDepositAsync(CreateProcessFeeDepositDTO createProcessFeeDepositDTO);
        Task UpdateProcessFeeDepositAsync(UpdateProcessFeeDepositDTO updateProcessFeeDepositDTO);
    }}