using System.Text.RegularExpressions;
using AngouriMath;
using AngouriMath.Core.Exceptions;
using Telegram.Bot.Types;

namespace TgBot.Utils;

public static class MathOperationsUtils
{
    private const string Pattern = @"(\-?)((?:\d*\.)?\d+)(\s*)([\^\+\-\*\/])(\s*)(\-?)((?:\d*\.)?\d+)";

    public static List<string> DoOperationsV2(Message? message)
    {
        if (message?.Text is null)
        {
            return new List<string>();
        }

        var text = message.Text;
        var exps = Regex.Matches(text, Pattern).Select(x => x.Value).ToList();
        var results = exps.Select(x => ((Entity)x).EvalNumerical()).ToList();

        var formatted = new List<string>();
        
        foreach (var t in results)
        {
            try
            {
                var cur = (double)t;
                var format = Math.Abs(cur - (int)cur) switch
                {
                    <= double.Epsilon => "",
                    > double.Epsilon => "0.00",
                    _ => throw new Exception("БЕДЫ С БАШКОЙ ТАМ ЛИБО МЕНЬШЕ ЛИБО БОЛЬШЕ ЛИБО РАВНО")
                };
                formatted.Add(cur.ToString(format));
            }
            catch (NumberCastException)
            {
                formatted.Add(t.ToString());
            }
        }

        return exps.Select((t, i) => $"{t} = {formatted[i]}").ToList();
    }

}