namespace Application.DTO
{
    public class LoanApplicationDTO
    {
        public int Id { get; set; }
        public string ApplicationNumber { get; set; } = string.Empty;

      
        public int ProductId { get; set; }
        public string? ProductName { get; set; } 

        public int BorrowerId { get; set; }
        public string? BorrowerName { get; set; } 

        public int ModalityId { get; set; }
        public string? ModalityName { get; set; } 

       
        public decimal RequestedAmount { get; set; }
        public int LoanTerm { get; set; }
        public string Purpose { get; set; } = string.Empty;

        
        public DateTime DateOfApplication { get; set; }
        public string Status { get; set; } = "Pending";

       
        public string? ApprovedById { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectionReason { get; set; }
        public DateTime? RejectionDate { get; set; }

        
        public List<GuarantorDTO> Guarantors { get; set; } = new();
        public List<ProvidedDocumentDTO> ProvidedDocuments { get; set; } = new();
    }
     public class CreateLoanApplicationDTO
    {
       
        public string ApplicationNumber { get; set; } = string.Empty;

      
        public int ProductId { get; set; }
        public string? ProductName { get; set; } 

        public int BorrowerId { get; set; }
        public string? BorrowerName { get; set; } 

        public int ModalityId { get; set; }
        public string? ModalityName { get; set; } 

       
        public decimal RequestedAmount { get; set; }
        public int LoanTerm { get; set; }
        public string Purpose { get; set; } = string.Empty;

        
        public DateTime DateOfApplication { get; set; }
        public string Status { get; set; } = "Pending";

       
        public string? ApprovedById { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectionReason { get; set; }
        public DateTime? RejectionDate { get; set; }

        
        public List<GuarantorDTO> Guarantors { get; set; } = new();
        public List<ProvidedDocumentDTO> ProvidedDocuments { get; set; } = new();

         public class UpdateLoanApplicationDTO
    {

        public int ProductId { get; set; }
        public string? ProductName { get; set; } 

        public int BorrowerId { get; set; }
        public string? BorrowerName { get; set; } 

        public int ModalityId { get; set; }
        public string? ModalityName { get; set; } 

       
        public decimal RequestedAmount { get; set; }
        public int LoanTerm { get; set; }
        public string Purpose { get; set; } = string.Empty;

        
        public DateTime DateOfApplication { get; set; }
        public string Status { get; set; } = "Pending";

       
        public string? ApprovedById { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? RejectionReason { get; set; }
        public DateTime? RejectionDate { get; set; }

        
        public List<GuarantorDTO> Guarantors { get; set; } = new();
        public List<ProvidedDocumentDTO> ProvidedDocuments { get; set; } = new();
    }
    }
}