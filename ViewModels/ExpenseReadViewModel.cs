namespace FinanceApp.ViewModels;

public class ExpenseReadViewModel
{
    public int Id { set; get; }
    public string Description { set; get; } = "";
    public double Amount { set; get; }
    public DateTime Date { set; get; }
}