namespace Application.DTO
{
    
    public partial class PaymentDTO
    {
        public int Id { get; set; }

        // --- Identification ---
        public int LoanDisbursementId { get; set; }
        public string? LoanReferenceNumber { get; set; } // e.g., "LN-2024-001"
        public string? BorrowerName { get; set; }       // Useful for the "All Payments" list

        // --- Financials ---
        public decimal TotalAmountPaid { get; set; }    // The total cash received

        // Waterfall Allocations (These should sum to TotalAmountPaid)
        public decimal PenaltyAllocated { get; set; }
        public decimal InterestAllocated { get; set; }
        public decimal PrincipalAllocated { get; set; }

        // --- Metadata ---
        public int PaymentTypeId { get; set; }
        public string? PaymentTypeName { get; set; }    // e.g., "Mobile Money", "Cash"
        
        public int AccountId { get; set; }
        public string? AccountName { get; set; }        // e.g., "Equity Bank Operating Acct"

        public string? TransactionReference { get; set; } // MoMo Ref or Bank Slip No.
        public DateTime PaymentDate { get; set; }
        
        public string? Remarks { get; set; }
        public string? CreatedBy { get; set; }          // The username of the staff who posted it
    }
}
    

    public record PaymentCreateDTO
    {
       public int LoanDisbursementId { get; set; }
        
        // The raw amount the borrower handed over (e.g., 50,000 RWF)
        public decimal TotalAmountPaid { get; set; }
        
        public int PaymentTypeId { get; set; } // Cash, MoMo, Bank Transfer
        public int AccountId { get; set; }     // Which company account received it
        
        public string? TransactionReference { get; set; } // The MoMo/Bank ID
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public string? Remarks { get; set; }
        
        // This helps track which staff member is logged in
        public string? CreatedBy { get; set; }
    }

    public record PaymentUpdateDTO
    {
      public int Id { get; set; } // The ID of the payment to change
        
        // Usually, we only allow updating non-financial metadata
        public string? TransactionReference { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? Remarks { get; set; }
        
        public int PaymentTypeId { get; set; }
        public int AccountId { get; set; }
        
        // If you absolutely MUST allow an amount update, 
        // you would need to re-run the entire Waterfall logic.
    }

    
