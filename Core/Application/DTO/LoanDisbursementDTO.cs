using Domain.Entities;
using Domain.ValueObjects;

namespace Application.DTO
{

    public class LoanDisbursementDTO
    {
        public int Id { get; set; }
        public int LoanApplicationId { get; set; }
        public string ApplicationNumber { get; set; } = string.Empty;
        public string BorrowerName { get; set; } = string.Empty;
        
        public decimal PrincipalDisbursed { get; set; }
        public decimal CurrentPrincipalBalance { get; set; }
        public decimal TotalInterestAccrued { get; set; }
        public decimal CurrentMonthInterest { get; set; }
        public decimal TotalPenaltiesAccrued { get; set; }
        public decimal CurrentMonthpenalty { get; set; }
        public decimal InterestRate { get; set; }
        
        public string PaymentMode { get; set; } = string.Empty;
        public string ReferenceNumber { get; set; } = string.Empty;
        
        public DateTime DisbursementDate { get; set; }
        public DateTime InterestClockStartDate { get; set; }
        public DateTime MaturityDate { get; set; }
        public DateTime? NextInstallmentDueDate { get; set; }
        
        public Domain.ValueObjects.LoanStatus Status { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // This holds the calculated list for the UI
        public List<RepaymentScheduleItemDTO> RepaymentSchedule { get; set; } = new();
    }

   public class RepaymentScheduleItemDTO
{
    public int Id { get; set; } // Added: Needed to link the penalty
    public int LoanDisbursementId { get; set; } // Added: Needed to link the penalty
    public int InstallmentNumber { get; set; }
    public DateTime DueDate { get; set; }
    public decimal PrincipalAmount { get; set; }
    public decimal InterestAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime? LastPenaltyDate { get; set; } // Added: Needed for the 30-day logic
}
}
  

    public class CreateLoanDisbursementDTO
    {
        public int LoanApplicationId { get; set; }
        public int AccountId { get; set; }
        
        public decimal PrincipalDisbursed { get; set; }
        
       
        public decimal InterestRate { get; set; }
        
       
        public string PaymentMode { get; set; } = string.Empty;
        
        
        public string ReferenceNumber { get; set; } = string.Empty;
        
        
        public DateTime DisbursementDate { get; set; } = DateTime.Now;

        
        public DateTime InterestClockStartDate { get; set; } = DateTime.Now;
        
        
        public DateTime MaturityDate { get; set; }

     
        public string DisbursedBy { get; set; } = string.Empty;
    }

    public class UpdateLoanDisbursementDTO
    {
        
        public string? ReferenceNumber { get; set; }
        
       
        public int Status { get; set; } 

       
        public string? Remarks { get; set; }
    }

   

    
