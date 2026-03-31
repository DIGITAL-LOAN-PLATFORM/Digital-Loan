using Domain.Entities;

public class Penalty
{
    public int Id { get; set; }
    
    public int LoanDisbursementId { get; set; }
    public virtual LoanDisbursement LoanDisbursement { get; set; }
    
    // Link to specific installment if applicable
    public int? RepaymentScheduleItemId { get; set; } 

    public decimal PenaltyAmount { get; set; } 
    public decimal AmountPaid { get; set; } = 0; 
    
    // Calculated: PenaltyAmount - AmountPaid
    public decimal CurrentBalance { get; set; } 

    public int ReasonId { get; set; }              
    public virtual Reason PenaltyReason { get; set; }      
    
    // Tracking the event
    public DateTime EventDate { get; set; } // The day the late payment occurred
    
    public PenaltyStatus Status { get; set; } = PenaltyStatus.Pending;
    
    public bool IsConfirmed { get; set; } = false; 
    public string? CreatedBy { get; set; } // The System or a Manager
    public string? ConfirmedByUserId { get; set; }
    public DateTime? ConfirmedAt { get; set; }
    // Ensure this is a string to match the "Active"/"Paid" check
        
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? Remarks { get; set; } // Why was this added?
}

public enum PenaltyStatus
{
    Pending = 1,  // Waiting for Manager approval
    Active = 2,   // Added to Loan Balance
    Paid = 3,     // Fully recovered
    Waived = 4    // Forgiven by management
}