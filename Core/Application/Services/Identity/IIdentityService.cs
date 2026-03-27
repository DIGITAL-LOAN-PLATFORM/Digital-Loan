using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTO;

namespace Application.Service
{
    public interface IIdentityService
    {
        Task RegisterUser(RegisterUserDTO dto);
         Task<List<UserDetailDTO>> GetAllUsers();
        Task<UserDetailDTO?> GetUserById(int id);
        Task UpdateUser(int id, UserUpdateDTO dto); // Changed to PascalCase
        Task<bool> LoginAsync(LoginDTO dto);
       Task LogoutAsync();
       
    }
}