using Application.DTO;
using Domain.Entities;
using Application.Interfaces;

namespace Application.Services.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly IAccount _account;

        public AccountService(IAccount account)
        {
            _account = account;
        }

        public async Task<List<Account>> GetAllAsync()
        {
            return await _account.GetAllAsync();
        }

        public async Task<Account> GetByIdAsync(int id)
        {
            return await _account.GetByIdAsync(id);
        }

        public async Task CreateAccountAsync(CreateAccountDTO createAccountDTO)
        {
            await _account.CreateAccountAsync(createAccountDTO);
        }

        public async Task UpdateAccountAsync(UpdateAccountDTO updateAccountDTO)
        {
            await _account.UpdateAccountAsync(updateAccountDTO);
        }

        public async Task DeleteAccountAsync(int id)
        {
            await _account.DeleteAccountAsync(id);
        }
    }
}