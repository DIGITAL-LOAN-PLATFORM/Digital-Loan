namespace Application.DTO
{
    public record ReasonDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public record CreateReasonDTO
    {
        public string Name { get; set; } = string.Empty;
    }

    public record UpdateReasonDTO
    {
        public string Name { get; set; } = string.Empty;
    }
}