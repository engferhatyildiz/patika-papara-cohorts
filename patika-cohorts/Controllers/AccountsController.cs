using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using patika_cohorts.Data;
using patika_cohorts.Models;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly BankingContext _context;

    public AccountsController(BankingContext context)
    {
        _context = context;

        if (_context.Accounts.Count() == 0)
        {
            // Örnek bir hesap ekleyelim
            _context.Accounts.Add(new Account { AccountNumber = "123456", Owner = "John Doe", Balance = 1000 });
            _context.SaveChanges();
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
    {
        return await _context.Accounts.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Account>> GetAccount(int id)
    {
        var account = await _context.Accounts.FindAsync(id);

        if (account == null)
        {
            return NotFound();
        }

        return account;
    }

    [HttpPost]
    public async Task<ActionResult<Account>> PostAccount(Account account)
    {
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, account);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAccount(int id, Account account)
    {
        if (id != account.Id)
        {
            return BadRequest();
        }

        _context.Entry(account).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAccount(int id)
    {
        var account = await _context.Accounts.FindAsync(id);

        if (account == null)
        {
            return NotFound();
        }

        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost("{id}/deposit")]
    public async Task<IActionResult> Deposit(int id, [FromBody] decimal amount)
    {
        var account = await _context.Accounts.FindAsync(id);

        if (account == null)
        {
            return NotFound();
        }

        account.Balance += amount;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost("{id}/withdraw")]
    public async Task<IActionResult> Withdraw(int id, [FromBody] decimal amount)
    {
        var account = await _context.Accounts.FindAsync(id);

        if (account == null)
        {
            return NotFound();
        }

        if (account.Balance < amount)
        {
            return BadRequest("Insufficient funds.");
        }

        account.Balance -= amount;
        await _context.SaveChangesAsync();

        return NoContent();
    }
}