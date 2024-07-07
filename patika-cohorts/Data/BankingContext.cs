using Microsoft.EntityFrameworkCore;
using patika_cohorts.Models;

namespace patika_cohorts.Data;

public class BankingContext : DbContext
{
    public BankingContext(DbContextOptions<BankingContext> options) : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; }
}