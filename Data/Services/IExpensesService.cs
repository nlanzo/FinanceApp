using FinanceApp.Models;
using FinanceApp.DTOs;

namespace FinanceApp.Data.Services;

public interface IExpensesService
{
    Task<IEnumerable<Expense>> GetAllExpenses();
    Task<Expense?> GetExpenseById(int id);
    Task AddExpense(Expense expense);
    Task<IEnumerable<ChartDataPoint>> GetChartData();
    Task UpdateExpense(Expense expense);
    Task DeleteExpense(int id);
}
