using System.ComponentModel.DataAnnotations;

namespace FinanceApp.ViewModels;

public class ExpenseEditViewModel
{
    public int? Id { set; get; }

    [Required]
    public string Description { set; get; } = null!;

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
    public double Amount { set; get; }

    [Required]
    public string Category { set; get; } = null!;
    public DateTime Date { set; get; } = DateTime.Now;
}
