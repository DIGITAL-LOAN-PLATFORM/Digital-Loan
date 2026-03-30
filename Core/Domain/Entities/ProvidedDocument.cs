namespace Domain.Entities
{
    public class ProvidedDocument
    {
        public int Id { get; set; }
        
        // Relationships - can be linked to either/both LoanApplication or LoanDisbursement
        public int? LoanApplicationId { get; set; }
        public virtual LoanApplication? LoanApplication { get; set; }
        
        public int? LoanDisbursementId { get; set; }
        public virtual LoanDisbursement? LoanDisbursement { get; set; }
        
        // Document information
        public string DocumentName { get; set; } = string.Empty; // "National ID", "Tax Certificate", etc.
        public int? RequiredDocumentId { get; set; } // Reference to required document type
        public virtual RequiredDocument? RequiredDocument { get; set; }
        
        // File metadata
        public string FileName { get; set; } = string.Empty; // Original file name
        public string FilePath { get; set; } = string.Empty; // Server storage path
        public long FileSize { get; set; } // File size in bytes
        public string ContentType { get; set; } = string.Empty; // MIME type (application/pdf, image/jpeg, etc.)
        
        // File content (optional - for small files)
        public byte[]? FileContent { get; set; }
        
        // Metadata
        public string Status { get; set; } = "Uploaded"; // Uploaded, Verified, Approved, Rejected
        public string? Remarks { get; set; }
        public string UploadedBy { get; set; } = string.Empty; // User who uploaded
        public DateTime UploadedDate { get; set; } = DateTime.UtcNow;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
