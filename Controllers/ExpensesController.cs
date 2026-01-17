using FinanceApp.Data.Services;
using FinanceApp.Models;
using Microsoft.AspNetCore.Mvc;
using FinanceApp.ViewModels;

namespace FinanceApp.Controllers;

public class ExpensesController : Controller
{
    private readonly IExpensesService _expensesService;
    public ExpensesController(IExpensesService expensesService)
    {
        _expensesService = expensesService;
    }

    public async Task<IActionResult> Index()
    {
        var expenses = await _expensesService.GetAllExpenses();
        return View(expenses);
    }

    public IActionResult Create()
    {
        var expenseViewModel = new ExpenseViewModel{
            Id = null,
            Amount = 0,
            Category = "",
            Date = DateTime.Now,
            Description = ""
        };
        return View("Update", expenseViewModel);
    }
    [HttpPost]
    public async Task<IActionResult> Create(ExpenseViewModel expense)
    {
        if(ModelState.IsValid)
        {
            var _expense = ExpenseViewModelToExpense(expense);

            await _expensesService.AddExpense(_expense);

            return RedirectToAction("Index");
        }
        return View("Update");
    }
    
    public async Task<IActionResult> GetChart(CancellationToken cancellationToken)
    {
        var data = await _expensesService.GetChartData(cancellationToken);
        return Json(data);
    }

    public async Task<IActionResult> Update(int id)
    {
        var expense = await _expensesService.GetExpenseById(id);
        var expenseViewModel = ExpenseToExpenseViewModel(expense);
        if (expense.Id == 0)
        {
            return NotFound();
        }
        return View(expenseViewModel);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(ExpenseViewModel expense)
    {
        if (expense.Id == null){
            return NotFound();
        }
        if(ModelState.IsValid)
        {
            var _expense = ExpenseViewModelToExpense(expense);
            await _expensesService.UpdateExpense(_expense);
            return RedirectToAction("Index");
        }
        return View(expense);
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _expensesService.DeleteExpense(id);
        return RedirectToAction("Index");
    }

    public IActionResult CreateFormPartial()
    {
        return PartialView("_CreateForm");
    }

    public Expense ExpenseViewModelToExpense(ExpenseViewModel expenseViewModel)
    {
        if (expenseViewModel.Id == null || expenseViewModel.Id == 0)
        {
            return new Expense{
                Amount = expenseViewModel.Amount,
                Category = expenseViewModel.Category ?? "",
                Description = expenseViewModel.Description ?? ""
            };
        }
        return new Expense{
            Id = expenseViewModel.Id.Value,
            Amount = expenseViewModel.Amount,
            Category = expenseViewModel.Category,
            Date = expenseViewModel.Date,
            Description = expenseViewModel.Description ?? ""
        };
    }

    public ExpenseViewModel ExpenseToExpenseViewModel(Expense expense)
    {
        return new ExpenseViewModel{
            Id = expense.Id,
            Amount = expense.Amount,
            Category = expense.Category ?? "",
            Date = expense.Date,
            Description = expense.Description ?? ""
        };
    }
}
