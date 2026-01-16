using FinanceApp.Models;

namespace FinanceApp.Data.Services;

public interface IExpensesService
{
    Task<IEnumerable<Expense>> GetAllExpenses();
    Task<Expense?> GetExpenseById(int id);
    Task AddExpense(Expense expense);
    IQueryable GetChartData();
    Task UpdateExpense(Expense expense);
}
