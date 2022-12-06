namespace TgBot.DTO;

public record ResponseCurrencyData(
    bool Success,
    long Timestamp,
    string Base,
    string Date,
    Dictionary<string, double> Rates);
