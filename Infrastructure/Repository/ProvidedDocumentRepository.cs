using Domain.Entities;
using Application.Interface;
using Application.DTO;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProvidedDocumentRepository : IProvidedDocument
    {
        private readonly ApplicationDbContext _context;

        public ProvidedDocumentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProvidedDocumentDTO?> GetByIdAsync(int id)
        {
            var doc = await _context.ProvidedDocuments
                .Include(d => d.LoanApplication)
                    .ThenInclude(la => la.Borrower)
                .Include(d => d.LoanDisbursement)
                    .ThenInclude(ld => ld.LoanApplication)
                        .ThenInclude(la => la.Borrower)
                .FirstOrDefaultAsync(d => d.Id == id);

            return doc == null ? null : MapToDto(doc);
        }

        public async Task<IEnumerable<ProvidedDocumentDTO>> GetByLoanApplicationIdAsync(int loanApplicationId)
        {
            var docs = await _context.ProvidedDocuments
                .Where(d => d.LoanApplicationId == loanApplicationId)
                .Include(d => d.LoanApplication)
                    .ThenInclude(la => la.Borrower)
                .Include(d => d.RequiredDocument)
                .AsNoTracking()
                .ToListAsync();

            return docs.Select(MapToDto);
        }

        public async Task<IEnumerable<ProvidedDocumentDTO>> GetByLoanDisbursementIdAsync(int loanDisbursementId)
        {
            var docs = await _context.ProvidedDocuments
                .Where(d => d.LoanDisbursementId == loanDisbursementId)
                .Include(d => d.LoanDisbursement)
                    .ThenInclude(ld => ld.LoanApplication)
                        .ThenInclude(la => la.Borrower)
                .Include(d => d.RequiredDocument)
                .AsNoTracking()
                .ToListAsync();

            return docs.Select(MapToDto);
        }

        public async Task<IEnumerable<ProvidedDocumentDTO>> GetAllAsync()
        {
            var docs = await _context.ProvidedDocuments
                .Include(d => d.LoanApplication)
                    .ThenInclude(la => la.Borrower)
                .Include(d => d.LoanDisbursement)
                    .ThenInclude(ld => ld.LoanApplication)
                        .ThenInclude(la => la.Borrower)
                .Include(d => d.RequiredDocument)
                .AsNoTracking()
                .ToListAsync();

            return docs.Select(MapToDto);
        }

        public async Task<bool> AddAsync(ProvidedDocument document)
        {
            await _context.ProvidedDocuments.AddAsync(document);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(ProvidedDocument document)
        {
            _context.Entry(document).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var doc = await _context.ProvidedDocuments.FindAsync(id);
            if (doc == null) return false;

            _context.ProvidedDocuments.Remove(doc);
            return await _context.SaveChangesAsync() > 0;
        }

        private ProvidedDocumentDTO MapToDto(ProvidedDocument doc)
        {
            return new ProvidedDocumentDTO
            {
                Id = doc.Id,
                LoanApplicationId = doc.LoanApplicationId,
                LoanDisbursementId = doc.LoanDisbursementId,
                LoanReferenceNumber = doc.LoanDisbursement?.ReferenceNumber ?? doc.LoanApplication?.ApplicationNumber ?? "N/A",
                BorrowerName = doc.LoanApplication?.Borrower != null
                    ? $"{doc.LoanApplication.Borrower.FirstName} {doc.LoanApplication.Borrower.LastName}"
                    : (doc.LoanDisbursement?.LoanApplication?.Borrower != null
                        ? $"{doc.LoanDisbursement.LoanApplication.Borrower.FirstName} {doc.LoanDisbursement.LoanApplication.Borrower.LastName}"
                        : "N/A"),
                DocumentName = doc.DocumentName,
                RequiredDocumentId = doc.RequiredDocumentId,
                RequiredDocumentType = doc.RequiredDocument?.DocumentName ?? "",
                FileName = doc.FileName,
                FilePath = doc.FilePath,
                FileSize = doc.FileSize,
                ContentType = doc.ContentType,
                Status = doc.Status,
                Remarks = doc.Remarks,
                UploadedBy = doc.UploadedBy,
                UploadedDate = doc.UploadedDate,
                CreatedAt = doc.CreatedAt
            };
        }
    }
}
