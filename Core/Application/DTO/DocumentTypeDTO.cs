namespace Application.DTO
{
    public class FilterDocumentTypeDTO
    {
        public string? SearchTerm { get; set; }
    }

    public class CreateDocumentTypeDTO
    {
        public string? DocumentName { get; set; }
    }

    public class UpdateDocumentTypeDTO
    {
        public string? DocumentName { get; set; }
    }
}