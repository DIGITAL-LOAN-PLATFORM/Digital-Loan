namespace Application.DTO
{
    public class LoanProductDTO
    {
         
        public string? ProductName { get; set; }
        public decimal? InterestRate { get; set; }
        public string? Description { get; set;}
        public DateTime CreatedAt { get; set; }
    }
}
