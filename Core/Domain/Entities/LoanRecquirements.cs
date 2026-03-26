namespace Domain.Entities
{

    public class LoanRecquirements{

        public int Id { get; set; }
      public RecquiredDocument? RecquiredDocument { get; set; } 
        public LoanProduct? LoanProduct { get; set; }
        
        public int RecquiredDocumentId { get; set; }
        public int LoanProductId { get; set; }
         public DateTime CreatedAt { get; set; }
    }

   
}