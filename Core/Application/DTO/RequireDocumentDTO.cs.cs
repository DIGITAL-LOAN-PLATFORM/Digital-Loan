using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Forms;
namespace Application.DTO
{
public class CreateRequiredDocumentDTO
    {
   
   public string DocumentName { get; set;}
   public int DocumentType { get; set;}
   public IBrowserFile DocumentFile { get; set;}
   
   

    }


    public class UpdateRequiredDocumentDTO
    {
  
   public string DocumentName { get; set;}
   public int DocumentType { get; set;}
   public string DocumentFile { get; set;}
     }
    public class FilterRequiredDocumentDTO
    {
         public string? SearchTerm { get; set; }
         public int DocumentName {get; set;}
         public string DocumentType { get; set;}
        


    }
}