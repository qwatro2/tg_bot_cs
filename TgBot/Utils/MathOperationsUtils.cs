using Telegram.Bot.Types;

namespace TgBot.Utils;

public static class MathOperationsUtils
{
    public static (int, bool) DoOperations(Message message)
    {
        var allMessageWords = message.Text.Split(' ');
        var operations = new List<char> { '+', '-', '*', '/' };

        var operationIndex = -1;
        var usingOperation = '.';
        for (var i = 0; i < allMessageWords.Length; i++)
        {
            foreach (var operation in operations)
            {
                if (allMessageWords[i].Contains(operation))
                {
                    operationIndex = i;
                    usingOperation = operation;
                    break;
                }
            }

            if (operationIndex != -1)
            {
                break;
            }
        }

        if (operationIndex == -1)
        {
            return (0, false);
        }

        string firstNumberString, secondNumberString;

        if (allMessageWords[operationIndex].Length == 1)
        {
            if (operationIndex == 0)
            {
                return (0, false);
            }

            firstNumberString = allMessageWords[operationIndex - 1];
            secondNumberString = allMessageWords[operationIndex + 1];
        }
        else
        {
            var numbers = allMessageWords[operationIndex].Split(usingOperation);
            if (numbers.Length != 2)
            {
                return (0, false);
            }

            firstNumberString = numbers[0];
            secondNumberString = numbers[1];
        }

        if (!int.TryParse(firstNumberString, out var firstNumber) ||
            !int.TryParse(secondNumberString, out var secondNumber))
        {
            return (0, false);
        }
        
        return usingOperation switch
        {
            '+' => (firstNumber + secondNumber, true),
            '-' => (firstNumber - secondNumber, true),
            '*' => (firstNumber * secondNumber, true),
            '/' => (firstNumber / secondNumber, true),
            _ => (int.MinValue, false)
        };
    }
}