using FinanceApp.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers;

public class CarsController : Controller
{
    private readonly ICarService _carService;

    public CarsController(ICarService carService)
    {
        _carService = carService;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var cars = await _carService.GetCarsAsync(cancellationToken);
        if (cars == null)
        {
            return View("OutsideServiceError");
        }
        return View(cars);
    }
}
