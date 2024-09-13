namespace dol.IoT.Models.Public.Auth;

public class LoginRequest
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    // public string? TwoFactorCode { get; init; }
    // public string? TwoFactorRecoveryCode { get; init; }
}