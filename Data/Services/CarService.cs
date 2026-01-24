using FinanceApp.DTOs;
using FinanceApp.Models;

namespace FinanceApp.Data.Services;

public class CarService : ICarService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<CarService> _logger;
    
    public CarService(IHttpClientFactory httpClientFactory, ILogger<CarService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }
    public async Task<List<CarDto>?> GetCarsAsync(CancellationToken cancellationToken)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient("CarApi");
            var response = await httpClient.GetFromJsonAsync<NhtsaResponse>("vehicles/GetMakesForVehicleType/car?format=json", cancellationToken);
            if (response?.Results == null)
            {
                return null;
            }

            return response.Results
                           .Select(item => new CarDto
                           {
                               MakeId = item.MakeId,
                               MakeName = item.MakeName
                           }).ToList();
        }
        catch (Exception ex)
        {
            // If something goes wrong I'm sending users to a generic error page
            _logger.LogError(ex, "Error occurred while fetching cars from NHTSA API");
            return null;
        }
    }
}