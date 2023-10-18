namespace JwtAuth.Models.Dtos;

public class RegisterDetails
{
    public string EmailAddress { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public int Age { get; set; }
}