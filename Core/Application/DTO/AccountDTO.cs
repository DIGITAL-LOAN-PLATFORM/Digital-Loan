namespace Application.DTO
{
    public class AccountDTO
    {
        public int Id { get; set; }
        public string AccountName { get; set; } = string.Empty;
        public string? Provider { get; set; }
        public string? Number { get; set; }
        public decimal Balance { get; set; }
    }

    public class CreateAccountDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Provider { get; set; } = string.Empty;
        public string Number { get; set;} = string.Empty;
        public int Type { get; set;}
        public decimal Balance { get; set;}
    }

    public class UpdateAccountDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Provider { get; set; } = string.Empty;
        public string Number { get; set;} = string.Empty;
        public int Type { get; set;}
        public decimal Balance { get; set;}
    }

    public class DeleteAccountDTO
    {
        public int Id { get; set; }
    }
}