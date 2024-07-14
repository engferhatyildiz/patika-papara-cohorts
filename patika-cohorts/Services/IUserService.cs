namespace patika_cohorts.Services;

public interface IUserService
{
    bool ValidateUser(string username, string password);
}