namespace patika_cohorts.Models;

public class Transaction
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public string TransactionType { get; set; }
    public DateTime Date { get; set; }

    public Account Account { get; set; }
}