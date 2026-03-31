namespace Application.DTO
{
   
    public class PenaltyDTO
    {
        public int Id { get; set; }
        public int LoanDisbursementId { get; set; }
        
        // Link to the specific month (Installment #)
        public int? RepaymentScheduleItemId { get; set; }
        public int InstallmentNumber { get; set; } 
        
        // Financials
        public decimal PenaltyAmount { get; set; } // The original 5% charge
        public decimal AmountPaid { get; set; }
        public decimal CurrentBalance { get; set; } // PenaltyAmount - AmountPaid
        
        // Metadata
        public string ReasonName { get; set; } = string.Empty;
        public DateTime EventDate { get; set; } // The date they hit "Day 1" late
        public string Status { get; set; } = string.Empty; // Active, Paid, Waived
        public string? Remarks { get; set; }
        
        // Audit
        public DateTime CreatedAt { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
    

   
