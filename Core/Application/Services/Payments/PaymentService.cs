using Domain.Entities;
using Application.Interface;
using Application.DTO;

namespace Application.Services.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IPayment paymentRepository; 

        public PaymentService(IPayment paymentRepository) 
        {
            this.paymentRepository = paymentRepository;
        }

        public async Task<Payment?> GetByIdAsync(int id)
        {
            return await paymentRepository.GetByIdAsync(id);
        }

        public async Task<List<Payment>> GetAllPaymentsAsync()
        {
            return await paymentRepository.GetAllPaymentsAsync();
        }

        public async Task<List<Payment>> GetPaymentsByLoanDisbursementIdAsync(int loanDisbursementId)
        {
            return await paymentRepository.GetPaymentsByLoanDisbursementIdAsync(loanDisbursementId);
        }

        public async Task CreatePaymentAsync(CreatePaymentDTO paymentDto)
        {
            await paymentRepository.CreatePaymentAsync(paymentDto);
        }

        public async Task UpdatePaymentAsync(int id, UpdatePaymentDTO paymentDto)
        {
            await paymentRepository.UpdatePaymentAsync(id, paymentDto);
        }

        public async Task DeletePaymentAsync(int id)
        {
            await paymentRepository.DeletePaymentAsync(id);
        }
    }
}