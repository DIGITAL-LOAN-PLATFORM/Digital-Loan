using Domain.Entities;
using Application.DTO;

namespace Application.Interface
{
    public interface IPaymentTypeService
    {
        Task<PaymentType?> GetByIdAsync(int id);
        Task<List<PaymentType>> GetAllPaymentTypesAsync();
        Task CreatePaymentTypeAsync(CreatePaymentTypeDTO paymentTypeDto);
        Task UpdatePaymentTypeAsync(int id, UpdatePaymentTypeDTO paymentTypeDto);
        Task DeletePaymentTypeAsync(int id);
    }
}