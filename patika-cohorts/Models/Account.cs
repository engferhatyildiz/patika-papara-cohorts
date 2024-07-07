namespace patika_cohorts.Models;

public class Account
{
    public int Id { get; set; }
    public string AccountNumber { get; set; }
    public string Owner { get; set; }
    public decimal Balance { get; set; }
}