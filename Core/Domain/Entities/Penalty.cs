namespace Domain.Entities
{
    public class Penalty
    {
        public int Id { get; set; }
        public int LoanDisbursementId { get; set; }
        // public int ApplicationId { get; set; }
        public decimal PenaltyAmount { get; set; }
        public string? ConfirmedByUserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
       
    }
}
