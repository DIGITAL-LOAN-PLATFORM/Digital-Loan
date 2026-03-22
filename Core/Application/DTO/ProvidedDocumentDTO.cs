namespace Application.DTO
{
    public class ProvidedDocumentDTO
    {
        public int Id { get; set; }
        
       
        public int LoanApplicationId { get; set; }

        
        public string DocumentName { get; set; } = string.Empty;

        
        public string DocumentFile { get; set; } = string.Empty;

       
        
       
        public string Extension => System.IO.Path.GetExtension(DocumentFile).ToLower();

        
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}