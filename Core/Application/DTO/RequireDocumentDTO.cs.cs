namespace Application.DTO
{
    public class CreateRequiredDocumentDTO
    {
        public string? DocumentName { get; set; }
        public string? DocumentType { get; set; }
       
    }

    public class UpdateRequiredDocumentDTO
    {
        public string? DocumentName { get; set; }
        public string? DocumentType { get; set; }

    }

    public class FilterRequiredDocumentDTO
    {
        public string? SearchTerm { get; set; }
        public string? DocumentName { get; set; }
        public string? DocumentType { get; set; }
    }
}