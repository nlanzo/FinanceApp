using FinanceApp.DTOs;

namespace FinanceApp.Data.Services;

public interface ICarService
{
    Task<List<CarDto>?> GetCars();
}