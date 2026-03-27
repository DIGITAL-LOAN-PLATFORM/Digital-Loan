using Domain.Entities;

namespace Application.Interface
{
    public interface IGuarantor
    {
        // --- Retrieval ---
        
        // Get a specific guarantor by ID with their Type and Application details
        Task<Guarantor?> GetByIdAsync(int id);

        // Get all guarantors for a specific Loan Application
        // This is crucial for your "Loan Details" page
        Task<IEnumerable<Guarantor>> GetByLoanApplicationIdAsync(int loanApplicationId);

        // Get all guarantors (for admin listing)
        Task<IEnumerable<Guarantor>> GetAllAsync();

        // Find a guarantor by their NIDA/ID Number (useful for blacklisting or duplicates)
        Task<Guarantor?> GetByIdentificationNumberAsync(string idNumber);

        // --- Persistence ---
        
        // Add a new guarantor to a loan
        Task<Guarantor> AddAsync(Guarantor guarantor);

        // Update guarantor details (e.g., if their phone number or address changes)
        Task UpdateAsync(Guarantor guarantor);

        // Remove a guarantor (only if the loan is still in 'Pending' status)
        Task DeleteAsync(int id);

        // --- Validation ---
        
        // Check if a specific NIDA is already guaranteeing a specific loan
        Task<bool> IsAlreadyGuarantorAsync(int loanApplicationId, string idNumber);
    }
}