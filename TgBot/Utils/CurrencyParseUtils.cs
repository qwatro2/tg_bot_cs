using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using TgBot.DTO;

namespace TgBot.Utils;

public static class CurrencyParseUtils
{
    private static readonly string CurrencyToken;

    static CurrencyParseUtils()
    {
        CurrencyToken = Environment.GetEnvironmentVariable("currencyToken") ??
                         throw new Exception("CurrencyToken missing");
    }

    public static async Task<ResponseCurrencyData?> GetAllCurrency()
    {
        var client = new RestClient("https://api.apilayer.com/exchangerates_data/" +
                                    "latest?symbols=USD,EUR,CNY&base=RUB");
        
        var request = new RestRequest();
        request.AddHeader("apikey", CurrencyToken);
        var response = await client.ExecuteAsync(request);
        
        var responseData = JsonConvert.DeserializeObject<ResponseCurrencyData>(response.Content);
        return responseData;
    }
}