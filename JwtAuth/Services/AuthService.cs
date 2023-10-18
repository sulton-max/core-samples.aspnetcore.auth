using System.IdentityModel.Tokens.Jwt;
using JwtAuth.Models.Dtos;
using JwtAuth.Models.Entities;

namespace JwtAuth.Services;

public class AuthService
{
    private readonly TokenGeneratorService _tokenGeneratorService;
    private readonly AccountService _accountService;
    private readonly JwtSecurityTokenHandler _jwtTokenHandler;

    public AuthService(TokenGeneratorService tokenGeneratorService, AccountService accountService)
    {
        _tokenGeneratorService = tokenGeneratorService;
        _accountService = accountService;
        _jwtTokenHandler = new JwtSecurityTokenHandler();
    }

    public ValueTask RegisterAsync(RegisterDetails registerDetails)
    {
        return _accountService.RegisterAsync(registerDetails);
    }

    public async ValueTask<string> LoginAsync(LoginDetails loginDetails)
    {
        // get user
        var user = await _accountService.LoginAsync(loginDetails);

        // generate jwt token
        return await GenerateTokenAsync(user);
    }

    private ValueTask<string> GenerateTokenAsync(User user)
    {
        var token = _tokenGeneratorService.GenerateToken(user);
        var tokenValue = _jwtTokenHandler.WriteToken(token);

        return ValueTask.FromResult(tokenValue);
    }
}