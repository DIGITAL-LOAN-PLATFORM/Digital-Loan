namespace Domain.Entities{

    public class Borrower{

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? IdentificationNumber { get; set;}
        public string? Email { get; set;}
        public string? PhoneNumber { get; set;}
        public DateTime? DOB { get; set;}
        public string? MaritalStatus { get; set;}
        public string? SpouseName { get; set;}
        public string? SpouseId { get; set;}
        public string? Address { get; set;}
        public DateTime CreatedAt { get; set; }
    }
}