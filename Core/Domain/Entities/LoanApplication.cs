using System.Runtime.InteropServices.Marshalling;

namespace Domain.Entities
{
    public class LoanApplication
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public LoanProduct loanProduct{ get; set; }
        public int BorrowerId { get; set; }
        public Borrower Borrower{ get; set; }
        public int ModalityId { get; set;}
        public PaymentModality paymentModality { get; set; }
        public decimal RequestedAmount { get; set;}
        public string Purpose {get; set; }
        public DateTime DateOfApplication { get; set; }
        public string? Status { get; set;} ="Pending";
        public DateTime PreferedDate { get; set; }
        public List<Guarantor> Guarantors { get; set; } = new();

    }
}
