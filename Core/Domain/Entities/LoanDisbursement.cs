using Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class LoanDisbursement
    {
        public int Id { get; set; }
        
        public int LoanApplicationId { get; set; }
        public virtual LoanApplication LoanApplication { get; set; } = null!;

        public int PaymentModalityId { get; set; }
        public virtual PaymentModality PaymentModality { get; set; } = null!;

        public decimal PrincipalDisbursed { get; set; } 
        public decimal InterestRate { get; set; } 
        public int DurationInMonths { get; set; }
        
        public string PaymentMode { get; set; } = string.Empty; 
        public string ReferenceNumber { get; set; } = string.Empty;
        public DateTime DisbursementDate { get; set; }

        public DateTime InterestClockStartDate { get; set; }
        public DateTime MaturityDate { get; set; } 

        // Properties used by Payment Waterfall logic
        public decimal PrincipalBalance { get; set; } 
        public decimal InterestBalance { get; set; }
        public decimal TotalPenaltiesAccrued { get; set; }
        
        public LoanStatus Status { get; set; } = LoanStatus.Active;
        
        public virtual ICollection<Penalty> Penalties { get; set; } = new List<Penalty>();
        public virtual ICollection<RepaymentScheduleItem> RepaymentSchedules { get; set; } = new List<RepaymentScheduleItem>();
        
        public string DisbursedBy { get; set; } = string.Empty; 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}