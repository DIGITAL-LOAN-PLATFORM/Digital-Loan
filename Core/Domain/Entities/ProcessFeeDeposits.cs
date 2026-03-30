namespace Domain.Entities
{
    public class ProcessFeeDeposit
    {
        public int Id { get; set; }
        public LoanApplication LoanApplication { get; set; }
        public float AmountPaid { get; set; }
        public PaymentType PaymentType { get; set; }
        public Account Account { get; set; }
        public DateTime DepositDate { get; set; }
        public string CustomerAccount { get; set; }

    }
}