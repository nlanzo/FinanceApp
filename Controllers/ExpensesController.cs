using FinanceApp.Data;
using FinanceApp.Data.Services;
using FinanceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<IActionResult> Create(Expense expense)
    {
        if(ModelState.IsValid)
        {
            await _expensesService.AddExpense(expense);

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
    public async Task<IActionResult> Update(Expense expense)
    {
        if(ModelState.IsValid)
        {
            await _expensesService.UpdateExpense(expense);
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
