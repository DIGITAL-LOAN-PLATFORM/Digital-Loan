namespace Domain.Entities
{
   
    public class RepaymentScheduleItem
    {
        public int Id { get; set; }
        public int LoanDisbursementId { get; set; }
        public DateTime DueDate { get; set; } // <--- Check this
        public bool IsPaid { get; set; }      // <--- Check this
        public decimal TotalAmount { get; set; }
        public decimal PrincipalAmount { get; set; }
        public decimal InterestAmount { get; set; }
        public int InstallmentNumber { get; set; }
        public DateTime? LastPenaltyDate { get; set; }
    }
}
    
