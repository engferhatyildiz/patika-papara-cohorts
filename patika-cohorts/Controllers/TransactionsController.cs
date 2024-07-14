using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using patika_cohorts.Attributes;
using patika_cohorts.Data;
using patika_cohorts.Models;

namespace patika_cohorts.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TransactionsController : ControllerBase
{
    private readonly BankingContext _context;

    public TransactionsController(BankingContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
    {
        return await _context.Transactions.Include(t => t.Account).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Transaction>> GetTransaction(int id)
    {
        var transaction = await _context.Transactions.Include(t => t.Account).FirstOrDefaultAsync(t => t.Id == id);

        if (transaction == null)
        {
            return NotFound();
        }

        return transaction;
    }

    [HttpPost]
    public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
    {
        var account = await _context.Accounts.FindAsync(transaction.AccountId);

        if (account == null)
        {
            return BadRequest("Invalid account ID.");
        }

        if (transaction.TransactionType == "Withdraw" && account.Balance < transaction.Amount)
        {
            return BadRequest("Insufficient funds.");
        }

        if (transaction.TransactionType == "Deposit")
        {
            account.Balance += transaction.Amount;
        }
        else if (transaction.TransactionType == "Withdraw")
        {
            account.Balance -= transaction.Amount;
        }
        else
        {
            return BadRequest("Invalid transaction type.");
        }

        transaction.Date = DateTime.UtcNow;
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTransaction), new { id = transaction.Id }, transaction);
    }
}