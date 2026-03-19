


namespace Domain.Entities
{
    public class Guarantor
    {
        public int Id { get; set; }
        public int IdentificationNumber { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int LoanApplicationId { get; set; }
        public LoanApplication LoanApplication { get; set; }
        public GuarantorType GuarantorType { get; set; }
    }
}