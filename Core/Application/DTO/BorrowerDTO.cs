namespace Application.DTO
{
    public record BorrowerDTO
    {
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
        // flat location fields to match entity
        public string? Province { get; set; }
        public string? District { get; set; }
        public string? Sector { get; set; }
        public string? Cell { get; set; }
        public string? Village { get; set; }
    }

    public record CreateBorrowerDTO
    {
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
        // flat location fields to match entity
        public string? Province { get; set; }
        public string? District { get; set; }
        public string? Sector { get; set; }
        public string? Cell { get; set; }
        public string? Village { get; set; }
    }

    public record UpdateBorrowerDTO
    {
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
        // flat location fields to match entity
        public string? Province { get; set; }
        public string? District { get; set; }
        public string? Sector { get; set; }
        public string? Cell { get; set; }
        public string? Village { get; set; }
    }
}