namespace Application.DTO
{
    public class LoanProductDTO
    {
         public int Id { get; set; }  
        public string? ProductName { get; set; }
        public decimal? InterestRate { get; set; }
        public string? Description { get; set;}
        public DateTime CreatedAt { get; set; }
    }
        public class CreateLoanProductDTO
    {
         
        public string? ProductName { get; set; }
        public decimal? InterestRate { get; set; }
        public string? Description { get; set;}
        public DateTime CreatedAt { get; set; }
    }
    public class UpdateLoanProductDTO
    {
        public int Id { get; set; } 
        public string? ProductName { get; set; }
        public decimal? InterestRate { get; set; }
        public string? Description { get; set;}
        public DateTime CreatedAt { get; set; }
    }
    
}
