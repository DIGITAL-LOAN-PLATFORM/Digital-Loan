using Application.DTO;
using Application.Interface; 
using Domain.Entities;


namespace Application.Services
{
    public class PaymentModalityService : IPaymentModalityService
    {
        private readonly IPaymentModality _paymentmodality;

        public PaymentModalityService(IPaymentModality paymentmodality)
        {
            _paymentmodality = paymentmodality;
        }

        public async Task<List<PaymentModality>> GetAllPaymentModalitiesAsync()
        {
            return await _paymentmodality.GetAllPaymentModalitiesAsync();
        }

        public async Task<PaymentModality?> GetByIdAsync(int id)
        {
            return await _paymentmodality.GetByIdAsync(id);
        }

        public async Task CreatePaymentModalityAsync(CreatePaymentModalityDTO dto)
        {
           
            await _paymentmodality.CreatePaymentModalityAsync(dto);
        }

        
        public async Task UpdatePaymentModalityAsync(int id, UpdatePaymentModalityDTO dto)
        {
            var existing = await _paymentmodality.GetByIdAsync(id);
            if (existing == null)
            {
                return;
            }

            await _paymentmodality.UpdatePaymentModalityAsync(id, dto);
        }
    }
}