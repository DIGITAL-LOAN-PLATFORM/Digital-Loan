namespace Domain.Entities
{

    public class LoanRecquirements{

        public int Id { get; set; }
        public string RecquirementDocument{ get; set; }
        public LoanProduct  LoanProduct { get; set; }
         public DateTime CreatedAt { get; set; }
    }

   
}