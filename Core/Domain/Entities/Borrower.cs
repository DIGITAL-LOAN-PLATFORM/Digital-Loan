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
        public string? SpouseId { get; set;}
         public DateTime CreatedAt { get; set; }

         //  Location Management 
    public string Province { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string Sector { get; set; } = string.Empty;
    public string Cell { get; set; } = string.Empty;
    public string Village { get; set; } = string.Empty;
    }
}