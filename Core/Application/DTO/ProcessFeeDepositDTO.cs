
using Domain.Entities;
namespace Application.DTO
{
    public class CreateProcessFeeDepositDTO
    {
        public LoanApplication LoanApplication { get; set; }
        public float AmountPaid { get; set; }
        public PaymentType PaymentType { get; set; }
        public Account Account { get; set; }
        public DateTime DepositDate { get; set; }
        public string CustomerAccount { get; set; }
    }
    public class UpdateProcessFeeDepositDTO
    {
     
        public LoanApplication LoanApplication { get; set; }
        public float AmountPaid { get; set; }
        public PaymentType PaymentType { get; set; }
        public Account Account { get; set; }
        public DateTime DepositDate { get; set; }
        public string CustomerAccount { get; set; }
    }
    
}