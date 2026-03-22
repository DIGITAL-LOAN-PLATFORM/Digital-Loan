using System.Runtime.InteropServices.Marshalling;

namespace Domain.Entities
{
    public class LoanApplication
    {
        public int Id { get; set; }
        
        public string ApplicationNumber { get; set; } = string.Empty;
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
       
        public string? ApprovedById { get; set; } // The ID of the Staff/Manager
        public DateTime? ApprovalDate { get; set; }
        public string? ApprovalRemarks { get; set; } // Notes from the manager


        public string? RejectionReason { get; set; }
        public DateTime? RejectionDate { get; set; }
       

    }
}
