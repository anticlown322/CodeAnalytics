﻿using System.Text;
namespace CoreTypes.Classes;

public class OperatorFinder
{
    private readonly string[] _nonСontroversialOperators =
    [
        "**", "==", "!=", "&&", "||", "~", ">>>", "+=", "-=",
        "*=", "/=", "%=", "<<=", ">>=", "&=", "^=", "|=", ".", "=>", 
        "foreach", "return", "case"
    ];
    private readonly string[] _сontroversialOperators =
    [
        "+", "-", "*", "/", "%", ">=", "<=", "!", "&", "|", "^", "<<", ">>", "for"
    ];
    
    private readonly string[] _veryСontroversialOperators =
    [
        "<", ">", "="
    ];
    
    private Metriс CountOccurrences(ref string text, string substring)
    {
        int count = 0;
        int index = 0;
        Metriс metriс = new (substring);
        StringBuilder resultText = new ();
        
        while ((index = text.IndexOf(substring, index, StringComparison.Ordinal)) != -1) 
        {
            count++;
            resultText.Append(text.Substring(0, index)).Append(new string('#', substring.Length));
            text = text.Substring(index + substring.Length);
            index = 0;
        }
        
        metriс.Count = count;
        resultText.Append(text);
        text = resultText.ToString();
        return metriс;
    }

    private Metriс[] CountWordOperators(ref string text)
    {
        int ifCount = CountOccurrences(ref text, "if").Count;
        int elseCount = CountOccurrences(ref text, "else").Count;
        int doCount = CountOccurrences(ref text, "do").Count;
        int whileCount = CountOccurrences(ref text, "while").Count;
        int matchCount = CountOccurrences(ref text, "match").Count;
        return
        [
            new Metriс("if") { Count = ifCount - elseCount },
            new Metriс("if..else") { Count = elseCount},
            new Metriс("while") { Count = whileCount - doCount},
            new Metriс("do..while") { Count = doCount},
            new Metriс("match..case") { Count = matchCount}
        ];
    }

    private Metriс[] CountBraсes(ref string text, int functionsCount)
    {
        int figureBraсeCount = CountOccurrences(ref text, "{").Count;
        int roundBraсeWithFunctionsCount = CountOccurrences(ref text, "(").Count;
        return
        [
            new Metriс("{..}") { Count = figureBraсeCount },
            new Metriс("(..)") { Count = roundBraсeWithFunctionsCount - functionsCount },
        ];
    }

    private (Dictionary<string, int>, int) CountFunctions(ref string text)
    {
        int quantity = 0;
        int i = 0;
        int leftShift = 0;
        Dictionary<string, int> functions = new();
        
        while (i < text.Length)
        {
            if (text[i] == '(')
            {
                do
                {
                    i--;
                    leftShift++;
                } while (i > 0 && text[i] == ' '); // идем влево, пока не кончатся пробелы
                
                if (char.IsLetterOrDigit(text[i])) // если текущий символ не буква/цифра, то это не название функции
                {
                    StringBuilder functionNameBuilder = new StringBuilder();
                    
                    while (i >= 0 && char.IsLetterOrDigit(text[i]))
                    {
                        functionNameBuilder.Append(text[i]);
                        i--;
                        leftShift++;
                    }
                    
                    string functionName = new string(functionNameBuilder.ToString().Reverse().ToArray()) + "()";
                    if (!functions.TryAdd(functionName, 1))
                    {
                        functions[functionName]++;
                    }

                    quantity++;
                }

                if (leftShift != 0)
                {
                    i += leftShift + 1; // +2, чтобы скипнуть "(", которая оказалась не функцией, и идти дальше 
                    leftShift = 0;
                }
            }
            i++;
        }
        
        return (functions, quantity);
    }

    public HashSet<Metriс> GetOperators(string text)
    {
        HashSet <Metriс> operators = new();
        
        foreach (var oper in _nonСontroversialOperators)
        {
            var info = CountOccurrences(ref text, oper);
            operators.Add(info);
        }
        foreach (var oper in _сontroversialOperators)
        {
            var info = CountOccurrences(ref text, oper);
            operators.Add(info);
        }
        foreach (var oper in _veryСontroversialOperators)
        {
            var info = CountOccurrences(ref text, oper);
            operators.Add(info);
        }
        
        Metriс[] wordOperators = CountWordOperators(ref text);
        foreach (var wordOper in wordOperators)
        {
            operators.Add(wordOper);
        }
        
        var functions = CountFunctions(ref text); //тут надо вернуть и словарь, и общее кол-во функций, чтобы передать их кол-во в метод для нахождения скобок
        foreach (var function in functions.Item1)
        {
            operators.Add(new Metriс(function.Key) {Count = function.Value});
        }
        
        wordOperators = CountBraсes(ref text, functions.Item2); // мы передаем кол-во функций, чтобы от кол-ва всех пар круглых скобок отнять те, что принадлежат функциям
        foreach (var wordOper in wordOperators)
        {
            operators.Add(wordOper);
        }

        //добавление index для тех operators, у которых Count > 0
        int index = 1;
        foreach (var oper in operators)
        {
            if (oper.Count > 0)
                oper.Index = index++;
        }

        return operators;
    }
}