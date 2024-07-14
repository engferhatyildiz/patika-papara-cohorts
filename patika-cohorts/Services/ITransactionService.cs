using patika_cohorts.Models;

namespace patika_cohorts.Services;

public interface ITransactionService
{
    Task<IEnumerable<Transaction>> GetTransactionsAsync();
    Task<Transaction> GetTransactionByIdAsync(int id);
    Task<Transaction> CreateTransactionAsync(Transaction transaction);   
}