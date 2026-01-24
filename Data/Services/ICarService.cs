using FinanceApp.DTOs;

namespace FinanceApp.Data.Services;

public interface ICarService
{
    /// <summary>
    /// Get the list of car makes from Nhtsa
    /// </summary>
    Task<List<CarDto>?> GetCarsAsync();
}
