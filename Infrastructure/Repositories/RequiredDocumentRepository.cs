using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using System.Text;

namespace Infrastructure.Repositories
{
    public class RequiredDocumentRepository : IRequiredDocument
    {
        private readonly ApplicationDbContext _dbContext;

        public RequiredDocumentRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public List<RequiredDocument> GetAllRequiredDocuments(FilterRequiredDocumentDTO filter)
        {
            IQueryable<RequiredDocument> query = _dbContext.RequiredDocuments;

            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                query = query.Where(c =>
                    (c.DocumentName != null && c.DocumentName.Contains(filter.SearchTerm)) ||
                    (c.DocumentType != null && c.DocumentType.Contains(filter.SearchTerm)));
            }

            if (!string.IsNullOrEmpty(filter.DocumentType))
            {
                query = query.Where(c => c.DocumentType == filter.DocumentType);
            }

            return query.ToList();
        }

        public RequiredDocument? GetRequiredDocumentById(int id)
        {
            return _dbContext.RequiredDocuments.FirstOrDefault(c => c.Id == id);
        }

        public void CreateRequiredDocument(CreateRequiredDocumentDTO dto)
        {
            var requiredDocument = new RequiredDocument
            {
                DocumentName = dto.DocumentName,
                DocumentType = dto.DocumentType,
                
                // DocumentFile = string.IsNullOrEmpty(dto.DocumentFile)
                //     ? null
                //     : Encoding.UTF8.GetBytes(dto.DocumentFile)
            };

            _dbContext.RequiredDocuments.Add(requiredDocument);
            _dbContext.SaveChanges();
        }

        public void UpdateRequiredDocument(int id, UpdateRequiredDocumentDTO dto)
        {
            var requiredDocument = _dbContext.RequiredDocuments.Find(id);
            if (requiredDocument == null) return;

            requiredDocument.DocumentName = dto.DocumentName;
            requiredDocument.DocumentType = dto.DocumentType;

           
            // if (!string.IsNullOrEmpty(dto.DocumentFile))
            // {
            //     requiredDocument.DocumentFile = Encoding.UTF8.GetBytes(dto.DocumentFile);
            // }

            _dbContext.SaveChanges();
        }
    }
}