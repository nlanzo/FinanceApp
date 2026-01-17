namespace FinanceApp.Models;
public class Expense
{
    public int Id { set; get; }
    public string Description { set; get; } = "";
    public double Amount { set; get; }
    public string? Category { set; get; } = null;
    public DateTime Date { set; get; } = DateTime.UtcNow;
}
