using Application.DTO;
using Application.Interfaces; // Where IPaymentService lives
using Application.Interface;  // Where IPayment and ILoanDisbursement repositories live
using Domain.Entities;
using Domain.ValueObjects;   
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPayment _paymentRepo;
        private readonly ILoanDisbursement _loanRepo;

        public PaymentService(IPayment paymentRepo, ILoanDisbursement loanRepo)
        {
            _paymentRepo = paymentRepo;
            _loanRepo = loanRepo;
        }

        // 1. Process Waterfall Payment (Already Implemented)
       public async Task<bool> ProcessLoanPaymentAsync(PaymentCreateDTO dto)
{
    // 1. Fetch the loan with all necessary details
    var loan = await _loanRepo.GetByIdWithPenaltiesAsync(dto.LoanDisbursementId);
    if (loan == null) return false;

    decimal remainingAmount = dto.TotalAmountPaid;
    
    var payment = new Payment
    {
        LoanDisbursementId = dto.LoanDisbursementId,
        TotalAmountPaid = dto.TotalAmountPaid,
        PaymentTypeId = dto.PaymentTypeId,
        AccountId = dto.AccountId,
        TransactionReference = dto.TransactionReference,
        PaymentDate = dto.PaymentDate,
        CreatedBy = dto.CreatedBy,
        Remarks = dto.Remarks,
        PenaltyAllocated = 0,
        InterestAllocated = 0,
        PrincipalAllocated = 0
    };

    // Step 1: Waterfall - Cover Interest First (from current/next due installment only)
    if (remainingAmount > 0)
    {
        decimal monthlyInterestOwed = 0;

        // Option 1: Use RepaymentSchedules if available
        if (loan.RepaymentSchedules != null && loan.RepaymentSchedules.Count > 0)
        {
            var nextInstallment = loan.RepaymentSchedules
                .Where(r => !r.IsPaid)
                .OrderBy(r => r.DueDate)
                .FirstOrDefault();

            if (nextInstallment != null && nextInstallment.InterestAmount > 0)
            {
                monthlyInterestOwed = nextInstallment.InterestAmount;
                
                // Allocate to this installment's interest
                decimal interestToPay = Math.Min(remainingAmount, monthlyInterestOwed);
                payment.InterestAllocated = interestToPay;
                
                nextInstallment.InterestAmount -= interestToPay;
                remainingAmount -= interestToPay;
            }
        }
        else
        {
            // Fallback: Calculate CONSTANT monthly interest from ORIGINAL PRINCIPAL DISBURSED
            // Monthly Interest = (Principal Disbursed × Interest Rate / 100) / 12
            // This amount stays the same every month throughout the loan term
            monthlyInterestOwed = (loan.PrincipalDisbursed * (loan.InterestRate / 100)) / 12;
            
            decimal interestToPay = Math.Min(remainingAmount, monthlyInterestOwed);
            if (interestToPay > 0)
            {
                payment.InterestAllocated = interestToPay;
                remainingAmount -= interestToPay;
            }
        }
    }

    // Step 2: Waterfall - Cover Penalties (Optional, keep if you want)
    if (remainingAmount > 0 && loan.Penalties != null)
    {
        var activePenalties = loan.Penalties
            .Where(p => p.Status == PenaltyStatus.Active)
            .OrderBy(p => p.EventDate);

        foreach (var penalty in activePenalties)
        {
            if (remainingAmount <= 0) break;
            decimal owed = penalty.PenaltyAmount - penalty.AmountPaid;
            decimal paid = Math.Min(remainingAmount, owed);
            
            penalty.AmountPaid += paid;
            payment.PenaltyAllocated += paid;
            remainingAmount -= paid;
            
            if (penalty.AmountPaid >= penalty.PenaltyAmount) 
                penalty.Status = PenaltyStatus.Paid;
        }
    }

    // Step 3: Principal (The Remainder) - from repayment schedules or PrincipalBalance
    if (remainingAmount > 0)
    {
        if (loan.RepaymentSchedules != null && loan.RepaymentSchedules.Count > 0)
        {
            // Use schedules if they exist
            var unpaidInstallments = loan.RepaymentSchedules
                .Where(r => !r.IsPaid || r.PrincipalAmount > 0)
                .OrderBy(r => r.DueDate)
                .ToList();

            foreach (var installment in unpaidInstallments)
            {
                if (remainingAmount <= 0) break;
                
                decimal principalToPay = Math.Min(remainingAmount, installment.PrincipalAmount);
                payment.PrincipalAllocated += principalToPay;
                
                installment.PrincipalAmount -= principalToPay;
                remainingAmount -= principalToPay;
                
                // Mark installment as paid if fully paid
                if (installment.PrincipalAmount <= 0 && installment.InterestAmount <= 0)
                    installment.IsPaid = true;
            }
        }
        else
        {
            // Fallback: Use loan's PrincipalBalance directly
            decimal principalToPay = Math.Min(remainingAmount, loan.PrincipalBalance);
            payment.PrincipalAllocated = principalToPay;
            loan.PrincipalBalance -= principalToPay;
            remainingAmount -= principalToPay;
        }
    }

    // Update Loan Status if fully paid
    bool allPaid = loan.RepaymentSchedules?.All(r => r.IsPaid) ?? 
                   (loan.PrincipalBalance <= 0 && loan.InterestBalance <= 0);
    if (allPaid)
    {
        loan.Status = LoanStatus.Settled;
    }

    // Save both the updated Loan and the new Payment record
    await _loanRepo.UpdateAsync(loan);
    return await _paymentRepo.AddAsync(payment);
}

        // 2. Get Payments by Loan ID (Already Implemented)
        public async Task<IEnumerable<PaymentDTO>> GetPaymentsByLoanIdAsync(int loanId)
        {
            var payments = await _paymentRepo.GetByLoanIdAsync(loanId);
            return payments.Select(p => new PaymentDTO {
                Id = p.Id,
                TotalAmountPaid = p.TotalAmountPaid,
                PenaltyAllocated = p.PenaltyAllocated,
                InterestAllocated = p.InterestAllocated,
                PrincipalAllocated = p.PrincipalAllocated,
                PaymentDate = p.PaymentDate,
                TransactionReference = p.TransactionReference,
                Remarks = p.Remarks
            });
        }

        // 3. Get Single Payment by ID (NEW - Required by Interface)
        public async Task<PaymentDTO?> GetPaymentByIdAsync(int id)
        {
            var p = await _paymentRepo.GetByIdAsync(id);
            if (p == null) return null;

            return new PaymentDTO
            {
                Id = p.Id,
                TotalAmountPaid = p.TotalAmountPaid,
                PenaltyAllocated = p.PenaltyAllocated,
                InterestAllocated = p.InterestAllocated,
                PrincipalAllocated = p.PrincipalAllocated,
                PaymentDate = p.PaymentDate,
                TransactionReference = p.TransactionReference,
                Remarks = p.Remarks
            };
        }

        // 4. Update Payment Details (NEW - Required by Interface)
        public async Task<bool> UpdatePaymentDetailsAsync(PaymentUpdateDTO updateDto)
        {
            var payment = await _paymentRepo.GetByIdAsync(updateDto.Id);
            if (payment == null) return false;

            // Only update editable tracking fields
            payment.TransactionReference = updateDto.TransactionReference;
            payment.Remarks = updateDto.Remarks;
            payment.PaymentTypeId = updateDto.PaymentTypeId;
            payment.AccountId = updateDto.AccountId;

            return await _paymentRepo.UpdateAsync(payment);
        }
        public async Task<IEnumerable<PaymentDTO>> GetAllPaymentsAsync()
{
    // Assuming your IPayment repo has a GetAllAsync or similar
    var payments = await _paymentRepo.GetAllAsync(); 
    return payments.Select(p => new PaymentDTO {
        Id = p.Id,
        TotalAmountPaid = p.TotalAmountPaid,
        PenaltyAllocated = p.PenaltyAllocated,
        InterestAllocated = p.InterestAllocated,
        PrincipalAllocated = p.PrincipalAllocated,
        PaymentDate = p.PaymentDate,
        TransactionReference = p.TransactionReference,
        LoanReferenceNumber = p.LoanDisbursement?.ReferenceNumber ?? "N/A",
        BorrowerName = p.LoanDisbursement?.LoanApplication?.Borrower != null 
            ? $"{p.LoanDisbursement.LoanApplication.Borrower.FirstName} {p.LoanDisbursement.LoanApplication.Borrower.LastName}" 
            : "N/A"
    }).OrderByDescending(x => x.PaymentDate);
}
    }
}