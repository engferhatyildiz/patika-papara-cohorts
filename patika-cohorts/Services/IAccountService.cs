using patika_cohorts.Models;

namespace patika_cohorts.Services;

public interface IAccountService
{
    Task<IEnumerable<Account>> GetAccountsAsync();
    Task<Account> GetAccountByIdAsync(int id);
    Task<Account> CreateAccountAsync(Account account);
    Task<Account> UpdateAccountAsync(Account account);
    Task<bool> DeleteAccountAsync(int id);
}