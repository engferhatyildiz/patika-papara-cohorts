using Microsoft.AspNetCore.Mvc;
using patika_cohorts.Attributes;
using patika_cohorts.Models;
using patika_cohorts.Services;


namespace patika_cohorts.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
    {
        var accounts = await _accountService.GetAccountsAsync();
        return Ok(accounts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Account>> GetAccount(int id)
    {
        var account = await _accountService.GetAccountByIdAsync(id);

        if (account == null)
        {
            return NotFound();
        }

        return Ok(account);
    }

    [HttpPost]
    public async Task<ActionResult<Account>> PostAccount(Account account)
    {
        var createdAccount = await _accountService.CreateAccountAsync(account);
        return CreatedAtAction(nameof(GetAccount), new { id = createdAccount.Id }, createdAccount);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAccount(int id, Account account)
    {
        if (id != account.Id)
        {
            return BadRequest();
        }

        await _accountService.UpdateAccountAsync(account);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAccount(int id)
    {
        var deleted = await _accountService.DeleteAccountAsync(id);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost("{id}/deposit")]
    public async Task<IActionResult> Deposit(int id, [FromBody] decimal amount)
    {
        var account = await _accountService.GetAccountByIdAsync(id);

        if (account == null)
        {
            return NotFound();
        }

        account.Balance += amount;

        // Transaction oluşturma
        var transaction = new Transaction
        {
            AccountId = id,
            Amount = amount,
            TransactionType = "Deposit",
            Date = DateTime.UtcNow
        };

        await _accountService.UpdateAccountAsync(account);
        return NoContent();
    }

    [HttpPost("{id}/withdraw")]
    public async Task<IActionResult> Withdraw(int id, [FromBody] decimal amount)
    {
        var account = await _accountService.GetAccountByIdAsync(id);

        if (account == null)
        {
            return NotFound();
        }

        if (account.Balance < amount)
        {
            return BadRequest("Insufficient funds.");
        }

        account.Balance -= amount;

        // Transaction oluşturma
        var transaction = new Transaction
        {
            AccountId = id,
            Amount = amount,
            TransactionType = "Withdraw",
            Date = DateTime.UtcNow
        };

        await _accountService.UpdateAccountAsync(account);
        return NoContent();
    }
}