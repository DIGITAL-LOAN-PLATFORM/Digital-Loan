namespace Application.DTO
{
    public record PaymentDTO
    {
        public int Id { get; set; }
        public int LoanDisbursementId { get; set; }
        public decimal TotalAmountPaid { get; set; }
        public decimal PenaltyAllocated { get; set; }
        public int PaymentTypeId { get; set; }   
        public string Account { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; }
    }

    public record CreatePaymentDTO
    {
        public int LoanDisbursementId { get; set; }
        public decimal TotalAmountPaid { get; set; }
        public decimal PenaltyAllocated { get; set; }
        public int PaymentTypeId { get; set; }   
        public string Account { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; }
    }

    public record UpdatePaymentDTO
    {
        public decimal TotalAmountPaid { get; set; }
        public decimal PenaltyAllocated { get; set; }
        public int PaymentTypeId { get; set; }   
        public string Account { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; }
    }
}