namespace Application.DTO
{
     public class GuarantorTypeDTO
    {
        public int Id {get; set; }
        public string Name { get; set; }
    }
    public class CreateGuarantorTypeDTO
    {
        public string Name { get; set; }
    }

    public class UpdateGuarantorTypeDTO
    {
        public string Name { get; set; }
    }
}