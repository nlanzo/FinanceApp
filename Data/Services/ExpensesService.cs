using FinanceApp.Models;
using FinanceApp.DTOs;
using Microsoft.EntityFrameworkCore;
using FinanceApp.ViewModels;

namespace FinanceApp.Data.Services;

public class ExpensesService : IExpensesService
{
    private readonly FinanceAppContext _context;
    public ExpensesService(FinanceAppContext context)
    {
        _context = context;
    }
    public async Task AddExpense(Expense expense)
    {
        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Expense>> GetAllExpenses()
    {
        var expenses = await _context.Expenses.ToListAsync();
        return expenses;
    }

    public async Task<ExpenseViewModel?> GetExpenseById(int id)
    {
        Expense? expenseModel = await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id);
        if (expenseModel == null)
        {
            return null;
        }
        return new ExpenseViewModel{
            Id = expenseModel.Id,
            Amount = expenseModel.Amount,
            Category = expenseModel.Category ?? "",
            Date = expenseModel.Date,
            Description = expenseModel.Description ?? ""
        };
    }

    public async Task<IEnumerable<ChartDataPoint>> GetChartData(CancellationToken cancellationToken)
    {
        var data = await _context.Expenses
                            .GroupBy(e => e.Category)
                            .Select(g => new ChartDataPoint
                            {
                                Category = g.Key ?? "Uncategorized",
                                Total = g.Sum(e => e.Amount)
                            })
                            .ToListAsync(cancellationToken);
        return data;
    }

    public async Task UpdateExpense(Expense expense)
    {
        _context.Expenses.Update(expense);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteExpense(int id)
    {
        var expense = await _context.Expenses.FindAsync(id);
        if (expense == null)
        {
            return;
        }
        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync();
    }
}
