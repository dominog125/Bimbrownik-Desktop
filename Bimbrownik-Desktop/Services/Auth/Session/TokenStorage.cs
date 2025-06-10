namespace Bimbrownik_Desktop.Services.Auth.Session;

public class TokenStorage
{
    private string? token;

    public void SaveToken(string newToken)
    {
        token = newToken;
    }

    public string? GetToken()
    {
        return token;
    }

    public void ClearToken()
    {
        token = null;
    }

    public bool IsAuthenticated => !string.IsNullOrWhiteSpace(token);
}