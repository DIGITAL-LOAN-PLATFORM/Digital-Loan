<<<<<<< HEAD
using Domain.Entities;
=======
>>>>>>> 8b26a51bdf784e04111b2993293fca12b6772dcf
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