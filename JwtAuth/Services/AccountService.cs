using JwtAuth.Models.Dtos;
using JwtAuth.Models.Entities;

namespace JwtAuth.Services;

public class AccountService
{
    public AccountService()
    {
    }

    public ValueTask RegisterAsync(RegisterDetails registerDetails)
    {
        // validate register details

        // store user in database

        return default;
    }

    public async ValueTask<User> LoginAsync(LoginDetails loginDetails)
    {
        // validate login details

        // get user from database
        var user = new User
        {
            Id = Guid.NewGuid(),
            EmailAddress = "john.doe@gmail.com",
            Password = "sdfsdf",
            Age = 30
        };

        return user;
    }
}