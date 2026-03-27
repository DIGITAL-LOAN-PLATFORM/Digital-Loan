namespace Domain.Entities
{
    public class Penalties
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public decimal PenaltyAmount { get; set; }
        public int ReasonId { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
    }
}
