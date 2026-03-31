using Application.DTO;
using Application.Interface;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.ProvidedDocuments
{
    public class ProvidedDocumentService : IProvidedDocumentService
    {
        private readonly IProvidedDocument _repository;

        public ProvidedDocumentService(IProvidedDocument repository)
        {
            _repository = repository;
        }

        public async Task<ProvidedDocumentDTO?> GetDocumentByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ProvidedDocumentDTO>> GetDocumentsByLoanApplicationAsync(int loanApplicationId)
        {
            return await _repository.GetByLoanApplicationIdAsync(loanApplicationId);
        }

        public async Task<IEnumerable<ProvidedDocumentDTO>> GetDocumentsByLoanDisbursementAsync(int loanDisbursementId)
        {
            return await _repository.GetByLoanDisbursementIdAsync(loanDisbursementId);
        }

        public async Task<IEnumerable<ProvidedDocumentDTO>> GetAllDocumentsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<bool> UploadDocumentAsync(CreateProvidedDocumentDTO dto)
        {
            // Validate that either LoanApplicationId or LoanDisbursementId is provided
            if (dto.LoanApplicationId == null && dto.LoanDisbursementId == null)
                return false;

            // Validate file
            if (string.IsNullOrEmpty(dto.FileName) || dto.FileSize <= 0)
                return false;

            // Create entity
            var document = new ProvidedDocument
            {
                LoanApplicationId = dto.LoanApplicationId,
                LoanDisbursementId = dto.LoanDisbursementId,
                DocumentName = dto.DocumentName,
                RequiredDocumentId = dto.RequiredDocumentId,
                FileName = dto.FileName,
                FilePath = GenerateFilePath(dto.FileName), // Generate path based on loan/date
                FileSize = dto.FileSize,
                ContentType = dto.ContentType,
                FileContent = dto.FileContent,
                Status = "Uploaded",
                UploadedBy = dto.UploadedBy,
                CreatedAt = DateTime.UtcNow
            };

            return await _repository.AddAsync(document);
        }

        public async Task<bool> UpdateDocumentStatusAsync(int id, string status, string? remarks)
        {
            var doc = await _repository.GetByIdAsync(id);
            if (doc == null) return false;

            // Map DTO back to entity (workaround - ideally we'd fetch the entity)
            var entity = new ProvidedDocument
            {
                Id = doc.Id,
                LoanApplicationId = doc.LoanApplicationId,
                LoanDisbursementId = doc.LoanDisbursementId,
                DocumentName = doc.DocumentName,
                RequiredDocumentId = doc.RequiredDocumentId,
                FileName = doc.FileName,
                FilePath = doc.FilePath,
                FileSize = doc.FileSize,
                ContentType = doc.ContentType,
                Status = status,
                Remarks = remarks,
                UploadedBy = doc.UploadedBy,
                UploadedDate = doc.UploadedDate,
                CreatedAt = doc.CreatedAt
            };

            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteDocumentAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        private string GenerateFilePath(string fileName)
        {
            // Generate file path: /documents/yyyy/MM/dd/filename-timestamp
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var date = DateTime.UtcNow;
            var directory = $"/documents/{date:yyyy}/{date:MM}/{date:dd}";
            var nameWithoutExt = System.IO.Path.GetFileNameWithoutExtension(fileName);
            var ext = System.IO.Path.GetExtension(fileName);
            
            return $"{directory}/{nameWithoutExt}-{timestamp}{ext}";
        }
    }
}
