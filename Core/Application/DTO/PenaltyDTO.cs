namespace Application.DTO
{
    public record PenaltyDTO
    {
        public int Id { get; set; }
        public int LoanDisbursementId { get; set; }
        public decimal PenaltyAmount { get; set; }
        public int ReasonId { get; set; }
        public string? ConfirmedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public record CreatePenaltyDTO
    {
        public int LoanDisbursementId { get; set; }
        public decimal PenaltyAmount { get; set; }
        public int ReasonId { get; set; }
        public string? ConfirmedByUserId { get; set; }
    }

    public record UpdatePenaltyDTO
    {
        public decimal PenaltyAmount { get; set; }
        public int ReasonId { get; set; }
        public string? ConfirmedByUserId { get; set; }
    }
}