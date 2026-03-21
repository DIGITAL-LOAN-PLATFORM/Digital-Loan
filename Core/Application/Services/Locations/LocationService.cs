using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Services.Locations
{
    public class LocationService : global::Application.Interface.ILocationService
    {
        private static List<RwandaLocation>? _staticCache = null;
        private static readonly SemaphoreSlim _cacheLock = new(1, 1);

        private async Task EnsureLoadedAsync()
        {
            if (_staticCache != null && _staticCache.Any()) return;

            await _cacheLock.WaitAsync();
            try
            {
                if (_staticCache != null && _staticCache.Any()) return;

                // VS Code / Dotnet Run often looks at the Project Root
                string[] paths = {
                    Path.Combine(AppContext.BaseDirectory, "wwwroot", "RwandaLocation.json"),
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "RwandaLocation.json"),
                    Path.Combine(Directory.GetCurrentDirectory(), "Web", "wwwroot", "RwandaLocation.json")
                };

                string? jsonPath = paths.FirstOrDefault(File.Exists);

                if (jsonPath == null)
                {
                    Console.WriteLine("DEBUG: Tried paths: " + string.Join(", ", paths));
                    _staticCache = new List<RwandaLocation>();
                    return;
                }

                var json = await File.ReadAllTextAsync(jsonPath);
                _staticCache = JsonSerializer.Deserialize<List<RwandaLocation>>(json, 
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<RwandaLocation>();
            }
            finally
            {
                _cacheLock.Release();
            }
        }

        public async Task<List<string>> GetProvincesAsync()
        {
            await EnsureLoadedAsync();
            return _staticCache!.Select(x => x.Province).Where(p => !string.IsNullOrEmpty(p)).Distinct().OrderBy(x => x).ToList();
        }

        public async Task<List<string>> GetDistrictsAsync(string province)
        {
            await EnsureLoadedAsync();
            return _staticCache!.Where(x => x.Province == province)
                                .Select(x => x.District).Where(d => !string.IsNullOrEmpty(d)).Distinct().OrderBy(x => x).ToList();
        }

        public async Task<List<string>> GetSectorsAsync(string district)
        {
            await EnsureLoadedAsync();
            return _staticCache!.Where(x => x.District == district)
                                .Select(x => x.Sector).Where(s => !string.IsNullOrEmpty(s)).Distinct().OrderBy(x => x).ToList();
        }

        public async Task<List<string>> GetCellsAsync(string sector)
        {
            await EnsureLoadedAsync();
            return _staticCache!.Where(x => x.Sector == sector)
                                .Select(x => x.Cell).Where(c => !string.IsNullOrEmpty(c)).Distinct().OrderBy(x => x).ToList();
        }

        public async Task<List<string>> GetVillagesAsync(string cell)
        {
            await EnsureLoadedAsync();
            return _staticCache!.Where(x => x.Cell == cell)
                                .Select(x => x.Village).Where(v => !string.IsNullOrEmpty(v)).Distinct().OrderBy(x => x).ToList();
        }
    }

    public class RwandaLocation
    {
        [JsonPropertyName("province_name")]
        public string Province { get; set; } = "";
        [JsonPropertyName("district_name")]
        public string District { get; set; } = "";
        [JsonPropertyName("sector_name")]
        public string Sector { get; set; } = "";
        [JsonPropertyName("cell_name")]
        public string Cell { get; set; } = "";
        [JsonPropertyName("village_name")]
        public string Village { get; set; } = "";
    }
}