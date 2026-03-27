using Domain.Entities;
using Application.DTO;

namespace Application.Interfaces
{
    public interface IAccount
    {
        Task<List<Account>> GetAllAsync();
        Task<Account> GetByIdAsync(int id);
        Task CreateAccountAsync(CreateAccountDTO createAccountDTO);
        Task UpdateAccountAsync(UpdateAccountDTO updateAccountDTO);
        Task DeleteAccountAsync(int id);
    }
}