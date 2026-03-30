using System.ComponentModel.DataAnnotations;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class LoanRequirements
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
        public int LoanProductId { get; set; }
        public LoanProduct? LoanProduct { get; set; }
        public bool IsMandatory { get; set; } = true;
        public int RequiredDocumentId { get; set; }
        public RequiredDocument? RequiredDocument { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

       
    }
}
