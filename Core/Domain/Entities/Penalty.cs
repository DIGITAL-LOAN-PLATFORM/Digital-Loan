namespace Domain.Entities
{
    public class Penalty
    {
        public int Id { get; set; }
        public int LoanDisbursementId { get; set; }
        public LoanDisbursement LoanDisbursement { get; set; }
        public decimal PenaltyAmount { get; set; }
        public int ReasonId { get; set; }              
        public Reason PenaltyReason { get; set; }      
        public string? ConfirmedByUserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}