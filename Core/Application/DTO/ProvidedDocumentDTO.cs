namespace Application.DTO
{
    public class ProvidedDocumentDTO
    {
        public int Id { get; set; }
        public int? LoanApplicationId { get; set; }
        public int? LoanDisbursementId { get; set; }
        public string? LoanReferenceNumber { get; set; }
        public string? BorrowerName { get; set; }
        
        public string DocumentName { get; set; } = string.Empty;
        public int? RequiredDocumentId { get; set; }
        public string? RequiredDocumentType { get; set; }
        
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public string ContentType { get; set; } = string.Empty;
        public string Extension => System.IO.Path.GetExtension(FileName).ToLower();
        
        public string Status { get; set; } = "Uploaded";
        public string? Remarks { get; set; }
        public string UploadedBy { get; set; } = string.Empty;
        public DateTime UploadedDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateProvidedDocumentDTO
    {
        public int? LoanApplicationId { get; set; }
        public int? LoanDisbursementId { get; set; }
        
        public string DocumentName { get; set; } = string.Empty;
        public int? RequiredDocumentId { get; set; }
        
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public byte[]? FileContent { get; set; }
        
        public string UploadedBy { get; set; } = string.Empty;
    }

    public class UpdateProvidedDocumentDTO
    {
        public int Id { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Remarks { get; set; }
    }
}