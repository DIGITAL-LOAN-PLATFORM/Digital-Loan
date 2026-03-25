namespace Domain.Entities
{
    public class ProvidedDocument
    {
        public int Id { get; set; }
        public int LoanId { get; set; }
        public string? DocumentName { get; set; }
        public int LoanApplicationId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentFile { get; set;}
        

       

        
    }
}
