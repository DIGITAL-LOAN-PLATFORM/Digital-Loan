using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Borrower
    {
        public int Id { get; set; }
        public string? IdentificationNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DOB { get; set; }
        public string? MaritalStatus { get; set; }
        public string? SpouseName { get; set; }
        public string? SpouseNidaNumber { get; set; }

        // location fields — made nullable to match DTO
        public string? Province { get; set; }
        public string? District { get; set; }
        public string? Sector { get; set; }   // ← fixed typo "Sectors" → "Sector"
        public string? Cell { get; set; }
        public string? Village { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}