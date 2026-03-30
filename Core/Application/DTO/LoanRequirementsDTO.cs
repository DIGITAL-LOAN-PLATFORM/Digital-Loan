namespace Application.DTO
{
    public class LoanRequirementsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int LoanProductId { get; set; }
        public string? ProductName { get; set; }
        public bool IsMandatory { get; set; } = true;
        public int RequiredDocumentId { get; set; }
        public string? DocumentName { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class CreateLoanRequirementsDTO
    {
        public string Name { get; set; } = string.Empty;
        public int LoanProductId { get; set; }
        public bool IsMandatory { get; set; } = true;
        public int RequiredDocumentId { get; set; }
    }

    public class UpdateLoanRequirementsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int LoanProductId { get; set; }
        public bool IsMandatory { get; set; }
        public int RequiredDocumentId { get; set; }
        public bool IsActive { get; set; }
    }
}
