namespace Application.DTO
{
    public record LocationDTO
    {
        public string Province { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Sector { get; set; } = string.Empty;
        public string Cell { get; set; } = string.Empty;
        public string Village { get; set; } = string.Empty;
    }
}
