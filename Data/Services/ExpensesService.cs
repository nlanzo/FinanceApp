using FinanceApp.DTOs;
using FinanceApp.Models;
using FinanceApp.ViewModels;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IEnumerable<ExpenseReadViewModel>> GetAllExpenses()
    {
        var query = _context.Expenses.Select(e => new ExpenseReadViewModel
        {
            Amount = e.Amount,
            Date = e.Date,
            Description = e.Description,
            Id = e.Id,
        });

        var data = await query.ToListAsync();

        return data;
    }

    public async Task<Expense> GetExpenseById(int id)
    {
        return await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id)
            ?? new Expense
            {
                Id = 0,
                Amount = 0,
                Category = null,
                Date = DateTime.UtcNow,
                Description = "",
            };
    }

    public async Task<IEnumerable<ChartDataPoint>> GetChartData(CancellationToken cancellationToken)
    {
        var data = await _context
            .Expenses.GroupBy(e => e.Category)
            .Select(g => new ChartDataPoint
            {
                Category = g.Key ?? "Uncategorized",
                Total = g.Sum(e => e.Amount),
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
