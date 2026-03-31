using Application.DTO;
using Application.Interfaces;
using Application.Interface;
using Application.Services;
using Domain.Entities;

namespace Application.Services.LoanApplications
{
    public class LoanApplicationService : ILoanApplicationService
    {
        private readonly ILoanApplication _repository;
        private readonly ILoanProductService _loanProductService;
        private readonly IPaymentModalityService _paymentModalityService;

        public LoanApplicationService(
            ILoanApplication repository,
            ILoanProductService loanProductService,
            IPaymentModalityService paymentModalityService)
        {
            _repository = repository;
            _loanProductService = loanProductService;
            _paymentModalityService = paymentModalityService;
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
            dto.ApplicationNumber = await GenerateNewApplicationNumberAsync();
            dto.DateOfApplication = DateTime.Now;
            dto.Status = "Pending";

            // Validate FKs
            var product = await _loanProductService.GetProductByIdAsync(dto.loanProductId);
            if (product == null)
            {
                throw new ArgumentException($"Loan product ID {dto.loanProductId} not found.");
            }

            var modality = await _paymentModalityService.GetByIdAsync(dto.paymentModalityId);
            if (modality == null)
            {
                throw new ArgumentException($"Payment modality ID {dto.paymentModalityId} not found.");
            }

            var entity = new LoanApplication
            {
                ApplicationNumber = dto.ApplicationNumber,
                loanProductId = dto.loanProductId,
                BorrowerId = dto.BorrowerId,
                paymentModalityId = dto.paymentModalityId,
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
            return $"LN-{year}-{(count + 1).ToString("D3")}";
        }

        private LoanApplicationDTO MapToDto(LoanApplication app)
        {
            return new LoanApplicationDTO
            {
                Id = app.Id,
                ApplicationNumber = app.ApplicationNumber,
                loanProductId = app.loanProductId,
                ProductName = app.loanProduct?.ProductName ?? "Unknown",
                ProductInterestRate = app.loanProduct?.InterestRate,
                BorrowerId = app.BorrowerId,
                BorrowerName = app.Borrower != null ? $"{app.Borrower.FirstName} {app.Borrower.LastName}" : $"ID: {app.BorrowerId}",
                paymentModalityId = app.paymentModalityId,
                ModalityName = app.paymentModality != null ? app.paymentModality.ToString() : "Unknown",
                RequestedAmount = app.RequestedAmount,
                LoanTerm = app.LoanTerm,
                Purpose = app.Purpose,
                Status = app.Status ?? "Pending",
                DateOfApplication = app.DateOfApplication,
                Guarantors = app.Guarantors.Select(g => new GuarantorDTO { Id = g.Id, Name = g.Name, Phone = g.Phone }).ToList(),
                ProvidedDocuments = app.ProvidedDocuments.Select(d => new ProvidedDocumentDTO { Id = d.Id, DocumentName = d.DocumentName }).ToList()
            };
        }
    }
}

