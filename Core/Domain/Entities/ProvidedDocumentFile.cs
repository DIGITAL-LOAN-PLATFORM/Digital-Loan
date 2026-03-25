namespace Domain.Entities
{
    public class ProvidedDocumentFile
    {
        public int Id { get; set; }
        public int IdProvidedDocument { get; set; }
        public string? FileName { get; set; }
        public string? ContentType { get; set; }
        public byte[]? FileData { get; set; }
        public ProvidedDocument? ProvidedDocument { get; set; }
    }
}