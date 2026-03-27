using Application.DTO;
using Domain.Entities;
using Application.Interfaces;

namespace Application.Services.Accounts
{
    public interface IAccountService
    {
        Task<List<Account>> GetAllAsync();
        Task<Account> GetByIdAsync(int id);
        Task CreateAccountAsync(CreateAccountDTO createAccountDTO);
        Task UpdateAccountAsync(UpdateAccountDTO updateAccountDTO);
        Task DeleteAccountAsync(int id);
    }
}