namespace C971.Services;

public static class LoginValidator
{
    public const string ValidUsername = "student";
    public const string ValidPassword = "wgu123";

    public static bool IsValid(string? username, string? password)
    {
        var normalizedUsername = username?.Trim() ?? string.Empty;
        var normalizedPassword = password ?? string.Empty;

        return normalizedUsername == ValidUsername &&
               normalizedPassword == ValidPassword;
    }
}