using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProvidedDocumentRepository : IProvidedDocument
    {
        private readonly ApplicationDbContext _dbContext;

        public ProvidedDocumentRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public List<ProvidedDocumentDTO> GetAllProvidedDocuments(FilterProvidedDocumentDTO filter)
        {
            IQueryable<ProvidedDocument> query = _dbContext.ProvidedDocuments
                .Include(c => c.DocumentFiles); // load related files

            if (!string.IsNullOrEmpty(filter.SearchTerm))
                query = query.Where(c =>
                    c.DocumentName != null && c.DocumentName.Contains(filter.SearchTerm));

            return query.Select(c => new ProvidedDocumentDTO
            {
                Id = c.Id,
                IdLoanApplication = c.IdLoanApplication,
                DocumentName = c.DocumentName,
                DocumentFiles = c.DocumentFiles.Select(f => new ProvidedDocumentFileDTO
                {
                    Id = f.Id,
                    FileName = f.FileName,
                    ContentType = f.ContentType
                }).ToList()
            }).ToList();
        }

        public ProvidedDocumentDTO? GetProvidedDocumentById(int id)
        {
            var entity = _dbContext.ProvidedDocuments
                .Include(c => c.DocumentFiles) // load related files
                .FirstOrDefault(c => c.Id == id);

            if (entity == null) return null;

            return new ProvidedDocumentDTO
            {
                Id = entity.Id,
                IdLoanApplication = entity.IdLoanApplication,
                DocumentName = entity.DocumentName,
                DocumentFiles = entity.DocumentFiles.Select(f => new ProvidedDocumentFileDTO
                {
                    Id = f.Id,
                    FileName = f.FileName,
                    ContentType = f.ContentType
                }).ToList()
            };
        }

        public async Task CreateProvidedDocumentAsync(CreateProvidedDocumentDTO dto)
        {
            var entity = new ProvidedDocument
            {
                IdLoanApplication = dto.IdLoanApplication,
                DocumentName = dto.DocumentName,
                DocumentFiles = new List<ProvidedDocumentFile>()
            };

            if (dto.DocumentFiles != null)
            {
                foreach (IBrowserFile file in dto.DocumentFiles)
                {
                    using var stream = new MemoryStream();
                    await file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024).CopyToAsync(stream);

                    entity.DocumentFiles.Add(new ProvidedDocumentFile
                    {
                        FileName = file.Name,
                        ContentType = file.ContentType,
                        FileData = stream.ToArray()
                    });
                }
            }

            _dbContext.ProvidedDocuments.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateProvidedDocumentAsync(int id, UpdateProvidedDocumentDTO dto)
        {
            var entity = _dbContext.ProvidedDocuments
                .Include(c => c.DocumentFiles) // load related files
                .FirstOrDefault(c => c.Id == id);

            if (entity == null) return;

            entity.DocumentName = dto.DocumentName;

            if (dto.DocumentFiles != null && dto.DocumentFiles.Any())
            {
                
                entity.DocumentFiles.Clear();

                foreach (IBrowserFile file in dto.DocumentFiles)
                {
                    using var stream = new MemoryStream();
                    await file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024).CopyToAsync(stream);

                    entity.DocumentFiles.Add(new ProvidedDocumentFile
                    {
                        FileName = file.Name,
                        ContentType = file.ContentType,
                        FileData = stream.ToArray()
                    });
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        public void DeleteProvidedDocument(int id)
        {
            var entity = _dbContext.ProvidedDocuments
                .Include(c => c.DocumentFiles)
                .FirstOrDefault(c => c.Id == id);

            if (entity == null) return;

            _dbContext.ProvidedDocuments.Remove(entity); 
            _dbContext.SaveChanges();
        }
    }
}