namespace TgBot.Utils;

public static class MathOperationsUtils
{
    public static int DoOperations(string[] commandData)
    {
        var firstNumber = int.Parse(commandData[1]);
        var secondNumber = int.Parse(commandData[3]);
        var operation = commandData[2];

        return operation switch
        {
            "+" => firstNumber + secondNumber,
            "-" => firstNumber - secondNumber,
            "*" => firstNumber * secondNumber,
            "/" => firstNumber / secondNumber,
            _ => 0
        };
    } 
}