using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
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

            // SAFETY: Check if filter is null before accessing properties
            if (filter != null)
            {
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
            }

            return query.AsNoTracking().ToList();
        }

        public RequiredDocument? GetRequiredDocumentById(int id)
        {
            return _dbContext.RequiredDocuments.AsNoTracking().FirstOrDefault(c => c.Id == id);
        }

        public void CreateRequiredDocument(CreateRequiredDocumentDTO dto)
        {
            var entity = new RequiredDocument
            {
                DocumentName = dto.DocumentName,
                DocumentType = dto.DocumentType
            };

            _dbContext.RequiredDocuments.Add(entity);
            _dbContext.SaveChanges();
        }

        public void UpdateRequiredDocument(int id, UpdateRequiredDocumentDTO dto)
        {
            var entity = _dbContext.RequiredDocuments.Find(id);
            if (entity == null) return;

            entity.DocumentName = dto.DocumentName;
            entity.DocumentType = dto.DocumentType;

            _dbContext.SaveChanges();
        }
    }
}