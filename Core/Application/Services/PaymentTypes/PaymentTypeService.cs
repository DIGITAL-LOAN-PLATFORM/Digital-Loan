using Domain.Entities;
using Application.Interface;
using Application.DTO;

namespace Application.Services.PaymentTypes
{
    public class PaymentTypeService : IPaymentTypeService
    {
        private readonly IPaymentType paymentTypeRepository;

        public PaymentTypeService(IPaymentType paymentTypeRepository)
        {
            this.paymentTypeRepository = paymentTypeRepository;
        }

        public async Task<PaymentType?> GetByIdAsync(int id)
        {
            return await paymentTypeRepository.GetByIdAsync(id);
        }

        public async Task<List<PaymentType>> GetAllPaymentTypesAsync()
        {
            return await paymentTypeRepository.GetAllPaymentTypesAsync();
        }

        public async Task CreatePaymentTypeAsync(CreatePaymentTypeDTO paymentTypeDto)
        {
            await paymentTypeRepository.CreatePaymentTypeAsync(paymentTypeDto);
        }

        public async Task UpdatePaymentTypeAsync(int id, UpdatePaymentTypeDTO paymentTypeDto)
        {
            await paymentTypeRepository.UpdatePaymentTypeAsync(id, paymentTypeDto);
        }

        public async Task DeletePaymentTypeAsync(int id)
        {
            await paymentTypeRepository.DeletePaymentTypeAsync(id);
        }
    }
}