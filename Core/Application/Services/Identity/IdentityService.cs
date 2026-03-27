using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTO;
using Application.Interface;
using Microsoft.Extensions.Logging;


namespace Application.Service
{
    public class IdentityService : IIdentityService
    {
        private readonly IIdentity _user;
        private readonly ILogger<IdentityService> _logger;
       

        public IdentityService(IIdentity user, ILogger<IdentityService> logger)
        {
            _user = user;
            _logger = logger;
        }

        public async Task RegisterUser(RegisterUserDTO dto)
        {
           await _user.RegisterUser(dto);
        }

       

       

        public async Task<List<UserDetailDTO>> GetAllUsers()
        {
            return await _user.GetAllUsers();
        }

        public async Task<UserDetailDTO?> GetUserById(int id)
        {
            return await _user.GetUserById(id);
        }

        public async Task UpdateUser(int id, UserUpdateDTO dto)
        {
            await _user.UpdateUser(id, dto);
          
        }
         public async Task<bool> LoginAsync(LoginDTO dto)
        {
            bool succeeded = await _user.LoginAsync(dto);
            if (succeeded)
            {
                _logger.LogInformation("User logged in: {Email}", dto.UserName);
            }
            else
            {
                _logger.LogWarning("Failed login attempt for user: {Email}", dto.UserName);
            }
            return succeeded;
        }

        public async Task LogoutAsync()
        {
            await _user.LogoutAsync();
            _logger.LogInformation("User logged out.");
        }
    }
}