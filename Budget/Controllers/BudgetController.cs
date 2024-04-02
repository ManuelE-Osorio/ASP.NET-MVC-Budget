using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Budget.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using System.Data;
using Microsoft.Extensions.FileProviders;

namespace Budget.Controllers;

public class BudgetController(BudgetContext context) : Controller
{
    private readonly BudgetContext _context = context;

    public async Task<IActionResult> Index()
    {
        if (_context.Transactions == null)
            return Problem("Entity set 'MvcMovieContext.Movie'  is null.");

        var transaction = await _context.Transactions.Include(p => p.Category).ToListAsync();
        var categories = await _context.Categories.ToListAsync();
        var viewModel = new IndexViewModel 
        {
            Categories = categories,
            Transactions = transaction
        };
        return View( viewModel );
    }

    [HttpPost]
    [Route("Budget/Transactions/Create/")]
    // [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateTransaction(
        [Bind("Name,Description,Date,Amount,Category"), FromBody] TransactionDTO transactionDto)
    {
        if (ModelState.IsValid && _context.Categories.Any( p => p.Name == transactionDto.Category ))  //check duplicate cat
        {
            var transaction = Transaction.FromDTO(transactionDto);
            transaction.Category = _context.Categories.FirstOrDefault( p => p.Name == transactionDto.Category)!;
            _context.Transactions.Add(transaction);

            await _context.SaveChangesAsync();
            return Created($"Budget/Transactions/{transaction.Id}", transaction);
        }
        return BadRequest();
    }

    [HttpDelete]
    [Route("Budget/Transactions/Delete/{id}")]
    // [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteTransaction(int id)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction != null)
            _context.Transactions.Remove(transaction);
        else
            return NotFound();
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPut]
    [Route("Budget/Transactions/Update/{id}")]
    // [ValidateAntiForgeryToken]  //Validate ModelState before id check
    public async Task<IActionResult> UpdateTransaction(int id, 
        [Bind("Id,Name,Description,Date,Amount,Category"), FromBody] TransactionDTO transactionDto)
    {
        if( id != transactionDto.Id)
            return BadRequest();

        Transaction transaction;

        if ( ModelState.IsValid && _context.Transactions.Any( p => p.Id == transactionDto.Id ))
        {
            if(_context.Categories.Any( p => p.Name == transactionDto.Category))
            {
                transaction = Transaction.FromDTO(transactionDto);
                transaction.Category = _context.Categories.FirstOrDefault( p => p.Name == transactionDto.Category)!;
            }
            else
                return BadRequest();

            try
            {
                _context.Transactions.Update(transaction);
                await _context.SaveChangesAsync();
            }
            catch(DBConcurrencyException)
            {
                return StatusCode(500);
            }
        }
        else
            return NotFound();

        return Ok(transaction);
    }

    [HttpPost]
    [Route("Budget/Categories/Create/")]
    // [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCategory([Bind("Name"), FromBody] Category category)
    {
        if (ModelState.IsValid)  // check duplicate cat
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return Created($"Budget/Categories/{category.Id}", category);
        }
        return BadRequest();
    }

    [HttpDelete]
    [Route("Budget/Categories/Delete/{id}")]
    // [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category =  _context.Categories.Find( id ) ;
        if (category != null)
            _context.Remove(category);
        else
            return NotFound();
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPut]
    [Route("Budget/Categories/Update/{id}")]
    // [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateCategory(int id, 
        [Bind("Id,Name"), FromBody] Category category)
    {
        if( id != category.Id)
            return BadRequest();

        if ( ModelState.IsValid && _context.Categories.Any( p => p.Id == category.Id ))
        {
            try
            {
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
            }
            catch(DBConcurrencyException)
            {
                return StatusCode(500);
            }
        }
        else
            return NotFound();

        return Ok(category);
    }
}