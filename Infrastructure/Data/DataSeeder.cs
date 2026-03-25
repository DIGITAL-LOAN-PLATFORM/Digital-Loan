// using Bogus;
// using Domain.Entities;
// using Microsoft.EntityFrameworkCore;


// namespace Infrastructure.Data
// {
//     public interface IDataSeeder
//     {
//         Task SeedAsync();
//     }

//     public class DataSeeder : IDataSeeder
//     {
//         private readonly ApplicationDbContext _context;
//         private const int MinRequiredDocumentCount = 40;
//         private const int MinGuestsPerRequiredDocument = 10;

//         public DataSeeder(ApplicationDbContext context)
//         {
//             _context = context;
//         }

//         public async Task SeedAsync()
//         {
//             var RequiredDocumentCount = await _context.RequiredDocuments.CountAsync();
//             // var guestCount = await _context.Guests.CountAsync();

//             // if (RequiredDocumentCount >= MinRequiredDocumentCount && guestCount >= MinRequiredDocumentCount * MinGuestsPerRequiredDocument)
//             //     return;

//             var RequiredDocuments = GenerateRequiredDocuments(MinRequiredDocumentCount);
//             await _context.RequiredDocuments.AddRangeAsync(RequiredDocuments);
//             await _context.SaveChangesAsync();
//         }

//         private List<RequiredDocument> GenerateRequiredDocuments(int count)
//         {
//             var RequiredDocumentFaker = new Faker<RequiredDocument>()
//                 .RuleFor(r => r.DocumentName, f => f.Random.Int(1, 5))
//                 .RuleFor(r => r.DocumentType, f => f.Random.AlphaNumeric(6).ToUpper())
//                 .RuleFor(r => r.DocumentFile, f => f.Random.Int(1, 4));
                
//             return RequiredDocumentFaker.Generate(count);
//         }
//         }
// }
