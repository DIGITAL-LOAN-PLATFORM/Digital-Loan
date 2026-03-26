namespace Domain.Entities
{
    public class Guarantor
    {
        public int Id { get; set; }
        public string? IdentificationNumber { get; set; }
        public string? Name { get; set; }
        public DateTime? DOB { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Province { get; set; }
        public string? District { get; set; }
        public string? Sector { get; set; }    // ← fixed typo: Sectors → Sector
        public string? Cell { get; set; }
        public string? Village { get; set; }

        public int LoanApplicationId { get; set; }
        public LoanApplication? LoanApplication { get; set; }

        public int GuarantorTypeId { get; set; }
        public GuarantorType? GuarantorType { get; set; }
    }
}