using Microsoft.EntityFrameworkCore;
using patika_cohorts.Data;
using patika_cohorts.Models;

namespace patika_cohorts.Services;

public class TransactionService : ITransactionService
{
    private readonly BankingContext _context;

    public TransactionService(BankingContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsAsync()
    {
        return await _context.Transactions.Include(t => t.Account).ToListAsync();
    }

    public async Task<Transaction> GetTransactionByIdAsync(int id)
    {
        return await _context.Transactions.Include(t => t.Account).FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }
}