using Microsoft.AspNetCore.Components.Forms;

namespace Application.DTO
{
    public class FilterProvidedDocumentDTO
    {
        public string? SearchTerm { get; set; }
    }

    public class CreateProvidedDocumentDTO
    {
        public int IdLoanApplication { get; set; }
        public string? DocumentName { get; set; }
        public IReadOnlyList<IBrowserFile>? DocumentFiles { get; set; }
    }

    public class UpdateProvidedDocumentDTO
    {
        public string? DocumentName { get; set; }
        public IReadOnlyList<IBrowserFile>? DocumentFiles { get; set; }
    }

    // Used as return type from repository and service
    public class ProvidedDocumentFileDTO
    {
        public int Id { get; set; }
        public string? FileName { get; set; }
        public string? ContentType { get; set; }
    }

    public class ProvidedDocumentDTO
    {
        public int Id { get; set; }
        public int IdLoanApplication { get; set; }
        public string? DocumentName { get; set; }
        public List<ProvidedDocumentFileDTO> DocumentFiles { get; set; } = new();
    }
}