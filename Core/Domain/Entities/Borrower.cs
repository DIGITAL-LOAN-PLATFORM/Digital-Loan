using Domain.ValueObjects;

namespace Domain.Entities{

    public class Borrower{

        public int Id { get; set; }
        public string? IdentificationNumber { get; set;}
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string? Email { get; set;}
        public string? PhoneNumber { get; set;}
        public DateTime? DOB { get; set;}
        public string? MaritalStatus { get; set;}
        public string? SpouseName { get; set;}
        public string? SpouseNidaNumber { get; set;}
        public Location HomeLocation { get; set; } 
         public bool IsActive { get; set; } = true; 

        // Audit fields required by the System of Record
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}