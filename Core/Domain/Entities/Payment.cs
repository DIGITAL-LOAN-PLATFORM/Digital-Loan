namespace Domain.Entities{

    public class Payment{

        public int Id { get; set; }
        public string Disbursement { get; set; }
        public  decimal AmountPaid  { get; set; }
        public string PaymentType{ get; set;}
        public string AccountName{ get; set;}
        public DateTime PaymentDate {get;set;}
        }
}