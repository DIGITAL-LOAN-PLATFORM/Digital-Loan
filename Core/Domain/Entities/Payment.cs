namespace Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int LoanDisbursementId { get; set; }
        public LoanDisbursement LoanDisbursement { get; set; } = null!;

        // The full amount the borrower handed over
        public decimal TotalAmountPaid { get; set; }

        // --- WATERFALL ALLOCATION ---
        // These three should always sum up to TotalAmountPaid
        public decimal PenaltyAllocated { get; set; }
        public decimal InterestAllocated { get; set; } // ADD THIS
        public decimal PrincipalAllocated { get; set; } // ADD THIS

        public int PaymentTypeId { get; set; } 
        public PaymentType PaymentType { get; set; } = null!;
        
        public int AccountId { get; set; }
        public Account Account { get; set; } = null!;

        public string? TransactionReference { get; set; } // ADD THIS for MoMo/Bank IDs
        public DateTime PaymentDate { get; set; }
        
        // Tracking who processed the payment
        public string? CreatedBy { get; set; } 
        public string? Remarks { get; set; }
    }
}