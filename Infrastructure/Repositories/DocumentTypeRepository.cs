using Application.DTO;
using Application.Interface;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class DocumentTypeRepository : IDocumentType
    {
        private readonly ApplicationDbContext _dbContext;

        public DocumentTypeRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public List<DocumentType> GetAllDocumentTypes(FilterDocumentTypeDTO filter)
        {
            IQueryable<DocumentType> query = _dbContext.DocumentTypes;

            if (!string.IsNullOrEmpty(filter.SearchTerm))
                query = query.Where(d =>
                    d.DocumentName != null && d.DocumentName.Contains(filter.SearchTerm));

            return query.ToList();
        }

        public DocumentType? GetDocumentTypeById(int id)
            => _dbContext.DocumentTypes.Find(id);

        public void CreateDocumentType(CreateDocumentTypeDTO dto)
        {
            _dbContext.DocumentTypes.Add(new DocumentType { DocumentName = dto.DocumentName });
            _dbContext.SaveChanges();
        }

        public void UpdateDocumentType(int id, UpdateDocumentTypeDTO dto)
        {
            var entity = _dbContext.DocumentTypes.Find(id);
            if (entity == null) return;
            entity.DocumentName = dto.DocumentName;
            _dbContext.SaveChanges();
        }

        public void DeleteDocumentType(int id)
        {
            var entity = _dbContext.DocumentTypes.Find(id);
            if (entity == null) return;
            _dbContext.DocumentTypes.Remove(entity);
            _dbContext.SaveChanges();
        }
    }
}