using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.Marshalling;

namespace Domain.Entities
{
    public class LoanApplication
    {
        public int Id { get; set; }
        [Required]
        public string ApplicationNumber { get; set; } = string.Empty;
        public int loanProductId { get; set; }
        public LoanProduct loanProduct { get; set; } = null!;
        public int BorrowerId { get; set; }
        public Borrower Borrower { get; set; } = null!;
        public int paymentModalityId { get; set; }
        public PaymentModality paymentModality { get; set; } = null!;
        public decimal RequestedAmount { get; set; }
        public int LoanTerm { get; set; }
        public string Purpose { get; set; } = string.Empty;
        public DateTime DateOfApplication { get; set; }
public string? Status { get; set; } = "Pending";
        public List<Guarantor> Guarantors { get; set; } = new();
        public List<ProvidedDocument> ProvidedDocuments { get; set; } = new();

       
        public string? ApprovedById { get; set; } // The ID of the Staff/Manager
        public DateTime? ApprovalDate { get; set; }
        public string? ApprovalRemarks { get; set; } // Notes from the manager


        public string? RejectionReason { get; set; }
        public DateTime? RejectionDate { get; set; }
       

    }
}
