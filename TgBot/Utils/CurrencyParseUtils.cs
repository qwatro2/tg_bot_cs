namespace TgBot.Utils;

public static class CurrencyParseUtils
{
    private static string _currencyToken;
    
    static CurrencyParseUtils()
    {
        _currencyToken = Environment.GetEnvironmentVariable("currencyToken") ?? throw new Exception("Token missing");
    }
}