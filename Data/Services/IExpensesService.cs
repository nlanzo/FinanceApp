using FinanceApp.Models;
using FinanceApp.DTOs;
using FinanceApp.ViewModels;

namespace FinanceApp.Data.Services;

public interface IExpensesService
{
    Task<IEnumerable<Expense>> GetAllExpenses();
    Task<ExpenseViewModel?> GetExpenseById(int id);
    Task AddExpense(Expense expense);
    Task<IEnumerable<ChartDataPoint>> GetChartData(CancellationToken cancellationToken);
    Task UpdateExpense(Expense expense);
    Task DeleteExpense(int id);
}
