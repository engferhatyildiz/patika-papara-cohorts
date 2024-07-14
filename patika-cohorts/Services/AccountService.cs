using Microsoft.EntityFrameworkCore;
using patika_cohorts.Data;
using patika_cohorts.Models;

namespace patika_cohorts.Services;

public class AccountService : IAccountService
{
    private readonly BankingContext _context;

    public AccountService(BankingContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Account>> GetAccountsAsync()
    {
        return await _context.Accounts.ToListAsync();
    }

    public async Task<Account> GetAccountByIdAsync(int id)
    {
        return (await _context.Accounts.FindAsync(id))!;
    }

    public async Task<Account> CreateAccountAsync(Account account)
    {
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();
        return account;
    }

    public async Task<Account> UpdateAccountAsync(Account account)
    {
        _context.Entry(account).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return account;
    }

    public async Task<bool> DeleteAccountAsync(int id)
    {
        var account = await _context.Accounts.FindAsync(id);
        if (account == null)
        {
            return false;
        }

        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync();
        return true;
    }
}