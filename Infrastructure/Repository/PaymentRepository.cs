using Application.Interface;
using Application.DTO;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PaymentRepository : IPayment
    {
        private readonly ApplicationDbContext _dbContext;

        public PaymentRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task<Payment?> GetByIdAsync(int id)
        {
            return await _dbContext.Payments
                .Include(p => p.LoanDisbursement)
                .Include(p => p.Account)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Payment>> GetAllPaymentsAsync()
        {
            return await _dbContext.Payments
                .Include(p => p.LoanDisbursement)
                .Include(p => p.Account)
                .ToListAsync();
        }

        public async Task<List<Payment>> GetPaymentsByLoanDisbursementIdAsync(int loanDisbursementId)
        {
            return await _dbContext.Payments
                .Include(p => p.LoanDisbursement)
                .Include(p => p.Account)
                .Where(p => p.LoanDisbursementId == loanDisbursementId)
                .ToListAsync();
        }

        public async Task CreatePaymentAsync(CreatePaymentDTO paymentDto)
        {
         var newPayment = new Payment
    {
        LoanDisbursementId = paymentDto.LoanDisbursementId,
        TotalAmountPaid = paymentDto.TotalAmountPaid,
        PenaltyAllocated = paymentDto.PenaltyAllocated,
        PaymentTypeId = paymentDto.PaymentTypeId,  
        PaymentDate = paymentDto.PaymentDate
    };

    await _dbContext.Payments.AddAsync(newPayment);
    await _dbContext.SaveChangesAsync();
}

     public async Task UpdatePaymentAsync(int id, UpdatePaymentDTO paymentDto)
{
    var existingPayment = await _dbContext.Payments.FindAsync(id);
    if (existingPayment == null) return;

    existingPayment.TotalAmountPaid = paymentDto.TotalAmountPaid;
    existingPayment.PenaltyAllocated = paymentDto.PenaltyAllocated;
    existingPayment.PaymentTypeId = paymentDto.PaymentTypeId;  
    existingPayment.PaymentDate = paymentDto.PaymentDate;

    await _dbContext.SaveChangesAsync();
}

        public async Task DeletePaymentAsync(int id)
        {
            var payment = await _dbContext.Payments.FindAsync(id);
            if (payment == null) return;

            _dbContext.Payments.Remove(payment);
            await _dbContext.SaveChangesAsync();
        }
    }
}