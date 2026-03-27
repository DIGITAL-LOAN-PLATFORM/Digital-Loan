using Domain.ValueObjects;

namespace Domain.Entities
{
    public class LoanDisbursement
    {
        public int Id { get; set; }
        
        // Relationship to the Approved Application
        public int LoanApplicationId { get; set; }
        public LoanApplication LoanApplication { get; set; }

       

        // Financial Snapshot at T=0 (Disbursement)
        public decimal PrincipalDisbursed { get; set; } 
        public decimal InterestRate { get; set; } // Fixed at time of disbursement
        public int DurationInMonths { get; set; }
        
        // Transaction Details
        public string PaymentMode { get; set; } // e.g., MoMo, Bank, Cash
        public string ReferenceNumber { get; set; } = string.Empty;
        public DateTime DisbursementDate { get; set; }

        // The "Interest Clock"
        public DateTime InterestClockStartDate { get; set; }
        public DateTime MaturityDate { get; set; } // When the loan MUST be finished

        // Current Standing (Live Ledger)
        public decimal CurrentPrincipalBalance { get; set; }
        public decimal TotalInterestAccrued { get; set; }
        public decimal TotalPenaltiesAccrued { get; set; }
        
        // Status Management
        public LoanStatus Status { get; set; } = LoanStatus.Active;
        
        // Audit Trail
        public string DisbursedBy { get; set; } // User ID of the Loan Officer/Manager
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

   
}