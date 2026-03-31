using Domain.ValueObjects;

namespace Application.DTO
{
public record Result<T>(bool IsSuccess, T? Data, string? Error)
{
    public static Result<T> Success(T data) => new(true, data, null);
    public static Result<T> Failure(string error) => new(false, default(T), error);
}

    public class GuarantorDTO
    {
        public int Id { get; set; }
        public string IdentificationNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime DOB { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        
        public Location ResidentialAddress { get; set; } = new(); 

        public int LoanApplicationId { get; set; }

        public int GuarantorTypeId { get; set; }
        public string? GuarantorTypeName { get; set; } 
    }

    public class CreateGuarantorDTO
    {
        public string IdentificationNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime DOB { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        
        public Location ResidentialAddress { get; set; } = new(); 

        public int LoanApplicationId { get; set; }

        public int GuarantorTypeId { get; set; }
        public string? GuarantorTypeName { get; set; } 
    }

    public class UpdateGuarantorDTO
    {
        public string Name { get; set; } = string.Empty;
        public DateTime DOB { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        
        public Location ResidentialAddress { get; set; } = new(); 

        public int GuarantorTypeId { get; set; }
        public string? GuarantorTypeName { get; set; } 
    }
}
