namespace Domain.Entities
{
<<<<<<< HEAD
    public class RecquiredDocument
=======
    public class RequiredDocument
>>>>>>> 8b26a51bdf784e04111b2993293fca12b6772dcf
    {
        public int Id { get; set; }
        public string? DocumentName { get; set; }    
        public string? DocumentType { get; set; }    
        public string? DocumentFile { get; set; }    
        public int? IdLoanProduct { get; set; }
        public LoanProduct? LoanProduct { get; set; }
    }
}