namespace dol.IoT.Models.Public.Auth;

public class ResetPasswordRequest
{
    public required string Email { get; init; }
    public required string ResetCode { get; init; }
    public required string NewPassword { get; init; }
}