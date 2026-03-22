using Domain.Entities;

namespace Domain.Entity
{
    public class LoanDisbursement
    {
        public int Id { get; set; }
        public LoanApplication LoanApplication { get; set; }
        public PaymentModality PaymentModality { get; set; }
        public float DisbursedAmount { get; set; }
        public string PaymentMode { get; set; }
        public string principalOffered { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float InterestRate { get; set; }
    }
}