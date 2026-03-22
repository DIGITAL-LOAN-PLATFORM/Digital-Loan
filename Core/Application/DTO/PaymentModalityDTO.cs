namespace Application.DTO
{
    public class PaymentModalityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; 
    }
    public class CreatePaymentModalityDTO
    {
        public string Modality { get; set; }
    }

    public class UpdatePaymentModalityDTO
    {
         public int Id { get; set; }
        public string Modality { get; set; }
    }
}