using System.Text.Json;
using Application.Interface;
using Domain.ValueObjects;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class JsonLocationService(
    IFileProvider webRootFileProvider,
    ILogger<JsonLocationService> logger) : ILocationService
{
    private const string LocationFilePath = "RwandaLocation.json";
    private List<Province>? _cachedLocations;
    private readonly SemaphoreSlim _cacheLock = new(1, 1);

    public async Task<List<Province>> GetAllLocationsAsync()
    {
        if (_cachedLocations is not null) return _cachedLocations;

        await _cacheLock.WaitAsync();
        try
        {
            if (_cachedLocations is not null) return _cachedLocations;

            var fileInfo = webRootFileProvider.GetFileInfo(LocationFilePath);
            if (!fileInfo.Exists)
            {
                logger.LogWarning("Location JSON file not found at {FilePath}.", LocationFilePath);
                _cachedLocations = [];
                return _cachedLocations;
            }

            await using var stream = fileInfo.CreateReadStream();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var locations = await JsonSerializer.DeserializeAsync<List<Province>>(stream, options);
            _cachedLocations = locations ?? [];
            return _cachedLocations;
        }
        catch (JsonException ex)
        {
            logger.LogError(ex, "Invalid JSON format in {FilePath}.", LocationFilePath);
            _cachedLocations = [];
            return _cachedLocations;
        }
        finally
        {
            _cacheLock.Release();
        }
    }

    public async Task<List<string>> GetProvincesAsync()
    {
        var all = await GetAllLocationsAsync();
        return all.Select(p => p.Name).ToList();
    }

    public async Task<List<string>> GetDistrictsAsync(string province)
    {
        var all = await GetAllLocationsAsync();
        return all.FirstOrDefault(p => p.Name == province)
                  ?.Districts.Select(d => d.Name).ToList() ?? [];
    }

    public async Task<List<string>> GetSectorsAsync(string district)
    {
        var all = await GetAllLocationsAsync();
        return all.SelectMany(p => p.Districts)
                  .FirstOrDefault(d => d.Name == district)
                  ?.Sectors.Select(s => s.Name).ToList() ?? [];
    }

    public async Task<List<string>> GetCellsAsync(string sector)
    {
        var all = await GetAllLocationsAsync();
        return all.SelectMany(p => p.Districts)
                  .SelectMany(d => d.Sectors)
                  .FirstOrDefault(s => s.Name == sector)
                  ?.Cells.Select(c => c.Name).ToList() ?? [];
    }

    public async Task<List<string>> GetVillagesAsync(string cell)
    {
        var all = await GetAllLocationsAsync();
        return all.SelectMany(p => p.Districts)
                  .SelectMany(d => d.Sectors)
                  .SelectMany(s => s.Cells)
                  .FirstOrDefault(c => c.Name == cell)
                  ?.Villages.Select(v => v.Name).ToList() ?? [];
    }
}