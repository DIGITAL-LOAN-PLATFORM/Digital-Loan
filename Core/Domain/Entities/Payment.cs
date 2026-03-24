
namespace Domain.Entities{

    public class Payment{

        public int Id { get; set; }
        public LoanDisbursement LoanDisbursement { get; set; }
        public  decimal AmountPaid  { get; set; }
        public PaymentType PaymentType{ get; set;}
        public Account Account{ get; set;}
        public DateTime PaymentDate {get;set;}
        }
}