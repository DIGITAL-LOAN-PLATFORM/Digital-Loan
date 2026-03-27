using Application.DTO;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class LoanApplicationService : ILoanApplicationService
    {
        private readonly ILoanApplication _repository;

        public LoanApplicationService(ILoanApplication repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<LoanApplicationDTO>> GetAllApplicationsAsync()
        {
            var apps = await _repository.GetAllAsync();
            return apps.Select(MapToDto);
        }

        public async Task<LoanApplicationDTO?> GetApplicationByIdAsync(int id)
        {
            var app = await _repository.GetByIdAsync(id);
            return app != null ? MapToDto(app) : null;
        }

        public async Task<IEnumerable<LoanApplicationDTO>> GetPendingApplicationsAsync()
        {
            var apps = await _repository.GetByStatusAsync("Pending");
            return apps.Select(MapToDto);
        }

        public async Task<LoanApplicationDTO> CreateApplicationAsync(LoanApplicationDTO dto)
        {
            // 1. Generate the unique ID (e.g., LN-2026-001)
            dto.ApplicationNumber = await GenerateNewApplicationNumberAsync();
            dto.DateOfApplication = DateTime.Now;
            dto.Status = "Pending";

            var entity = new LoanApplication
            {
                ApplicationNumber = dto.ApplicationNumber,
                ProductId = dto.ProductId,
                BorrowerId = dto.BorrowerId,
                ModalityId = dto.ModalityId,
                RequestedAmount = dto.RequestedAmount,
                LoanTerm = dto.LoanTerm,
                Purpose = dto.Purpose,
                DateOfApplication = dto.DateOfApplication,
                Status = dto.Status
            };

            var savedEntity = await _repository.AddAsync(entity);
            dto.Id = savedEntity.Id;
            return dto;
        }

        public async Task<bool> UpdateApplicationStatusAsync(int id, string newStatus, string? remarks = null)
        {
            var app = await _repository.GetByIdAsync(id);
            if (app == null) return false;

            app.Status = newStatus;
            
            if (newStatus == "Approved")
            {
                app.ApprovalDate = DateTime.Now;
                app.ApprovalRemarks = remarks;
            }
            else if (newStatus == "Rejected")
            {
                app.RejectionDate = DateTime.Now;
                app.RejectionReason = remarks;
            }

            await _repository.UpdateAsync(app);
            return true;
        }

        public async Task<string> GenerateNewApplicationNumberAsync()
        {
            int year = DateTime.Now.Year;
            int count = await _repository.GetCountForYearAsync(year);
            // Formats to: LN-2026-001, LN-2026-002, etc.
            return $"LN-{year}-{(count + 1).ToString("D3")}";
        }

        // Internal Helper for Mapping
      private LoanApplicationDTO MapToDto(LoanApplication app)
{
    return new LoanApplicationDTO
    {
        Id = app.Id,
        ApplicationNumber = app.ApplicationNumber,
        ProductId = app.ProductId,
        ProductName = app.loanProduct?.ProductName,
        BorrowerId = app.BorrowerId,
        
        // FIX: Concatenate FirstName and LastName
        BorrowerName = app.Borrower != null 
            ? $"{app.Borrower.FirstName} {app.Borrower.LastName}" 
            : "Unknown Borrower",

        RequestedAmount = app.RequestedAmount,
        LoanTerm = app.LoanTerm,
        Status = app.Status ?? "Pending",
        DateOfApplication = app.DateOfApplication,
        
        Guarantors = app.Guarantors.Select(g => new GuarantorDTO 
        { 
            Id = g.Id, 
            Name = g.Name,
            Phone = g.Phone 
        }).ToList()
    };
}
    }
}