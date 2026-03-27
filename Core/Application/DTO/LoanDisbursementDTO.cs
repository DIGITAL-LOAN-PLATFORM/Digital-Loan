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
        public int PaymentModalityId { get; set; }
        public PaymentModality? PaymentModality { get; set; }

        public decimal PrincipalDisbursed { get; set; } 
        public decimal InterestRate { get; set; } 
        public int DurationInMonths { get; set; }
        
        public string PaymentMode { get; set; } = string.Empty;
        public string ReferenceNumber { get; set; } = string.Empty;
        public DateTime DisbursementDate { get; set; }

        public DateTime InterestClockStartDate { get; set; }
        public DateTime MaturityDate { get; set; } 

        public decimal CurrentPrincipalBalance { get; set; }
        public decimal TotalInterestAccrued { get; set; }
        public decimal TotalPenaltiesAccrued { get; set; }
        
        public LoanStatus Status { get; set; } = LoanStatus.Active;
        public string StatusName { get; set; } = string.Empty;
        
        public string DisbursedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class CreateLoanDisbursementDTO
    {
        public int LoanApplicationId { get; set; }
        // public string LoanApplicationName {get; set; }
        // public int PaymentModalityId { get; set; }
        // public string PaymentModalityName {get; set; }
        
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

   
}
    
