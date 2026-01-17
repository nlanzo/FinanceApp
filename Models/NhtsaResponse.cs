using FinanceApp.DTOs;

namespace FinanceApp.Models;

public class NhtsaResponse {
    public int Count { get; set; }
    public string Message { get; set; } = null!;
    public List<CarDto>? Results { get; set; }
}