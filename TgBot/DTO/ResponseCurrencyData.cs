namespace TgBot.DTO;

public record ResponseCurrencyData
{
    public bool Success { get; init; }
    public long Timestamp { get; init; }
    public string Base { get; init; }
    public string Date { get; init; }
    public Dictionary<string, double> Rates { get; init; }
};