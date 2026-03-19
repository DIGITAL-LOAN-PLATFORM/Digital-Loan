namespace Domain.Entities{

    public class Account{

        public int Id { get; set; }
        public Disbursement Disbursement { get; set; }
        public  decimal AmountPaid  { get; set; }
        public PaymentType PaymentType{ get; set;}
        public Account Account{ get; set;}
        public DateTime PaymentDate {get;set;}
        }
}