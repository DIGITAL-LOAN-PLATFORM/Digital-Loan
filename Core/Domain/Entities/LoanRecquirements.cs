namespace Domain.Entities
{

    public class LoanRecquirements{

        public int Id { get; set; }
        public RequirementDocument RequirementDocument{ get; set; }
        public LoanProduct  LoanProduct { get; set; }
        public int RequirementDocumentId { get; set; }
        public int LoanProductId { get; set; }
         public DateTime CreatedAt { get; set; }
    }

   
}