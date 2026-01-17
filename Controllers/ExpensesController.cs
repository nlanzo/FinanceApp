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
        ViewData["Title"] = "Create Expense";
        ViewData["Action"] = "Create";
        return View("Update");
    }
    [HttpPost]
    public async Task<IActionResult> Create(ExpenseViewModel expense)
    {
        if(ModelState.IsValid)
        {
            var _expense = new Expense{
                Amount = expense.Amount,
                Category = expense.Category,
                Description = expense.Description
            };

            await _expensesService.AddExpense(_expense);

            return RedirectToAction("Index");
        }
        ViewData["Title"] = "Create Expense";
        ViewData["Action"] = "Create";
        return View("Update");
    }
    
    public async Task<IActionResult> GetChart(CancellationToken cancellationToken)
    {
        var data = await _expensesService.GetChartData(cancellationToken);
        return Json(data);
    }

    public async Task<IActionResult> Update(int id)
    {
        ViewData["Title"] = "Update Expense";
        ViewData["Action"] = "Update";
        var expense = await _expensesService.GetExpenseById(id);
        if (expense == null)
        {
            return NotFound();
        }
        return View(expense);
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
            var _expense = new Expense{
                Id = expense.Id.Value,
                Amount = expense.Amount,
                Category = expense.Category,
                Date = expense.Date,
                Description = expense.Description
            };
            await _expensesService.UpdateExpense(_expense);
            return RedirectToAction("Index");
        }
        ViewData["Title"] = "Update Expense";
        ViewData["Action"] = "Update";
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
}
