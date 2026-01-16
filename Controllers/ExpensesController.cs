using FinanceApp.Data;
using FinanceApp.Data.Services;
using FinanceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Controllers
{
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
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Expense expense)
        {
            if(ModelState.IsValid)
            {
                await _expensesService.AddExpense(expense);

                return RedirectToAction("Index");
            }
            return View();
        }
        
        public IActionResult GetChart()
        {
            var data = _expensesService.GetChartData();
            return Json(data);
        }

        public async Task<IActionResult> Update(int id)
        {
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
            return View(expense);
        }
    }
}