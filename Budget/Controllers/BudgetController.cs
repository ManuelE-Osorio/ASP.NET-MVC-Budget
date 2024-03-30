using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Budget.Models;
using System.Threading.Tasks;

namespace Budget.Controllers;

public class BudgetController(BudgetContext context) : Controller
{
    private readonly BudgetContext _context = context;

    public async Task<IActionResult> Index()
    {
        if (_context.Transactions == null)
        {
            return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
        }
        
        return View(await _context.Transactions.ToListAsync());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Description,Date,Amount,Category")] Transaction transaction)
    {
        if (ModelState.IsValid)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Index));
    }
}