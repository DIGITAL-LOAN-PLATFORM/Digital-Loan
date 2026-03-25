namespace Domain.Entities
{
    public class ProvidedDocument
    {
        public int Id { get; set; }
        public int IdLoanApplication { get; set; }
        public string? DocumentName { get; set; }
        public List<ProvidedDocumentFile> DocumentFiles { get; set; } = new();
    }
}