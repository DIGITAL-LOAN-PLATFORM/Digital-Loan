namespace Domain.Entities
{

    public class Account
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Provider { get; set; }
        public string? Number { get; set;}
public int? Type { get; set;}
        public decimal? Balance { get; set;}
        public DateTime CreatedAt { get; set; }
    }
}