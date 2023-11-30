using System.Collections.Generic;

public class TokenBlacklistService
{
    private static HashSet<string> _blacklistedTokens = new HashSet<string>();

    public static void AddToBlacklist(string token)
    {
        _blacklistedTokens.Add(token);
    }

    public static bool IsTokenBlacklisted(string token)
    {
        return _blacklistedTokens.Contains(token);
    }
}
