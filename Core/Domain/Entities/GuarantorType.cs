using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
   public class GuarantorType
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    } 
}