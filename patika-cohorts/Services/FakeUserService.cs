namespace patika_cohorts.Services;

public class FakeUserService : IUserService
{
    public bool ValidateUser(string username, string password)
    {
        // Simple fake user validation
        return username == "ferhatyildiz" && password == "cohorts";
    }
}