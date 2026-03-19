namespace Domain.Entities
{
    public class LoanApplication
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int ModalityId { get; set;}
        public decimal RequestedAmount { get; set;}
        public DateTime DateOfApplication { get; set; }
        public string? Status { get; set;}
        public DateTime PreferedDate { get; set; }

    }
}
