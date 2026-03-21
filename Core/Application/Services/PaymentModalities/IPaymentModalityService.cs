using Application.DTO;
using Domain.Entities;

namespace Application.Services
{
    public interface IPaymentModalityService
    {
         Task<List<PaymentModality>> GetAllPaymentModalitiesAsync();
        Task<PaymentModality?> GetByIdAsync(int id);
        Task CreatePaymentModalityAsync(CreatePaymentModalityDTO dto);
        Task UpdatePaymentModalityAsync(int id, UpdatePaymentModalityDTO dto);
    }

   
}