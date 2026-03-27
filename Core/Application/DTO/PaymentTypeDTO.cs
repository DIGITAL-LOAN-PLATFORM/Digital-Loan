namespace Application.DTO
{
    public record PaymentTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public record CreatePaymentTypeDTO
    {
        public string Name { get; set; } = string.Empty;
    }

    public record UpdatePaymentTypeDTO
    {
        public string Name { get; set; } = string.Empty;
    }
}