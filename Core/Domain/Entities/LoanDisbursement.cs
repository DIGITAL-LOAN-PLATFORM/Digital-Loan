

namespace Domain.Entities
{
    public class LoanDisbursement
    {
        public int Id { get; set; }
        public int LoanApplicationId { get; set; }
        public LoanApplication LoanApplication { get; set; }
        public int PaymentModalityId { get; set; }
        public PaymentModality PaymentModality { get; set; }
        public float DisbursedAmount { get; set; }
        public string PaymentMode { get; set; }
        public string ReferenceNumber { get; set; } = string.Empty;
        public string principalOffered { get; set; }
        public DateTime DisbursementDate { get; set; }
        public DateTime InterestClockStartDate { get; set; }
        public DateTime InterestClockEndDate { get; set; }
        public float InterestRate { get; set; }
    }
}