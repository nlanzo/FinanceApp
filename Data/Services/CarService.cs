using FinanceApp.DTOs;
using FinanceApp.Models;

namespace FinanceApp.Data.Services;

public class CarService : ICarService
{
    private readonly HttpClient _httpClient;
    public CarService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<List<CarDto>?> GetCars()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<NhtsaResponse>("vehicles/GetMakesForVehicleType/car?format=json");
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
        // If something goes wrong I'm sending users to a generic error page
        // logging to console for now but would normally use csharp logger
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}