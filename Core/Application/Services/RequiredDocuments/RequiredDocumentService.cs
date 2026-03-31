using Application.Interfaces;
using Application.DTO;
using Domain.Entities;

namespace Application.Services.RequiredDocuments
{
    public class RequiredDocumentService : IRequiredDocumentService
    {
        private readonly IRequiredDocument _repository;

        public RequiredDocumentService(IRequiredDocument repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Fetches all documents based on the provided filter.
        /// Handles the null-filter scenario to prevent NullReferenceExceptions in the repository.
        /// </summary>
        public async Task<List<RequiredDocument>> GetAllRequiredDocumentsAsync(FilterRequiredDocumentDTO? filter = null)
        {
            // Safety Check: If the UI passes null, we provide an empty DTO 
            // so the repository doesn't crash when accessing filter.SearchTerm
            filter ??= new FilterRequiredDocumentDTO();

            // We wrap the synchronous repository call in Task.Run to keep the UI thread responsive
            return await Task.Run(() => _repository.GetAllRequiredDocuments(filter));
        }

        /// <summary>
        /// Retrieves a single document by its unique identifier.
        /// </summary>
        public async Task<RequiredDocument?> GetRequiredDocumentByIdAsync(int id)
        {
            return await Task.Run(() => _repository.GetRequiredDocumentById(id));
        }

        /// <summary>
        /// Maps the Create DTO to a new entity via the repository.
        /// Returns 1 to indicate success (or you can modify the repository to return the new ID).
        /// </summary>
        public async Task<int> CreateRequiredDocumentAsync(CreateRequiredDocumentDTO dto)
        {
            return await Task.Run(() =>
            {
                _repository.CreateRequiredDocument(dto);
                return 1; 
            });
        }

        /// <summary>
        /// Updates an existing document's details.
        /// </summary>
        public async Task UpdateRequiredDocumentAsync(int id, UpdateRequiredDocumentDTO dto)
        {
            await Task.Run(() => _repository.UpdateRequiredDocument(id, dto));
        }

        /// <summary>
        /// Logic for deleting a document (if implemented in your repository).
        /// </summary>
        public async Task DeleteRequiredDocumentAsync(int id)
        {
            // If your IRequiredDocument interface doesn't have Delete yet, 
            // you'll need to add it there and in the Repository first.
            await Task.CompletedTask; 
        }
    }
}