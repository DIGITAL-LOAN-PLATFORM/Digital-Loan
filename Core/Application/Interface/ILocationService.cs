using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface ILocationService
    {
        Task<List<string>> GetProvincesAsync();
        Task<List<string>> GetDistrictsAsync(string province);
        Task<List<string>> GetSectorsAsync(string district);
        Task<List<string>> GetCellsAsync(string sector);
        Task<List<string>> GetVillagesAsync(string cell);
    }
}
