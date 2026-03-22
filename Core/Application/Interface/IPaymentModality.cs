using Application.DTO;
using Domain.Entities;

namespace Application.Interface
{
    public interface IPaymentModality
    {
        Task<List<PaymentModality>> GetAllPaymentModalitiesAsync(); 
        Task<PaymentModality?> GetByIdAsync(int id);
        Task CreatePaymentModalityAsync(CreatePaymentModalityDTO dto);
        Task UpdatePaymentModalityAsync(int id, UpdatePaymentModalityDTO dto);
    }
}