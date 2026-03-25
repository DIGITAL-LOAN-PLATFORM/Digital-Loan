namespace Domain.Entities
{
    public class RequiredDocument
    {
        public int Id { get; set; }
        public string? DocumentName { get; set; }    
        public string? DocumentType { get; set; }    
        public string? DocumentFile { get; set; }    
        public int? IdLoanProduct { get; set; }
        public LoanProduct? LoanProduct { get; set; }
    }
}