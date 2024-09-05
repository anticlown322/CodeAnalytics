using System.Text;

namespace Lexer;

public class Lexer
{
    private static readonly string[] NonСontroversialOperators = new[]
    {
        "**", "==", "!=", "&&", "||", "~", ">>>", "+=", "-=",
        "*=", "/=", "%=", "<<=", ">>=", "&=", "^=", "|=", ".", "for", "return"
    };
    private static readonly string[] СontroversialOperators = new[]
    {
        "+", "-", "*", "/", "%", ">=", "<=", "!", "&", "|", "^", "<<", ">>"
    };
    
    private static readonly string[] VeryСontroversialOperators = new[]
    {
        "<", ">", "="
    };
    
    class OperatorInfo
    {
        public string name { get; set; }
        public int count { get; set; }
    }

    private static string CleanText(string text)
    {
        StringBuilder textBuilder = new StringBuilder();
        int i = 0; 
        while (i < text.Length - 1)
        {
            if (text[i] == '"') // удаление содержимого строк, оставляя кавычки
            {
                textBuilder.Append(text[i++]);
                while (text[i] != '"')
                {
                    i++;
                }
            } 
            if (text[i] == '/' && text[i + 1] == '*') // удаление многострочных комментариев 
            {
                while (!(text[i] == '*' && text[i + 1] == '/'))
                {
                    i++;
                }
                
                i += 2;
            }

            if (text[i] == '/' && text[i + 1] == '/') // удаление однострочных комментариев 
            {
                while (text[i] != '\n')
                {
                    i++;
                }
            }

            if (text[i] == '\'') //удаление содержимого символа, оставля кавычки      'a' => ''
            {
                textBuilder.Append(text[i]);
                i += 2;
            }
            textBuilder.Append(text[i]);
            i++;
        }

        textBuilder.Append(text[i]);
        return textBuilder.ToString();
    }
    
    private static int CountOccurrences(ref string text, string substring)
    {
        int count = 0;
        int index = 0;
        StringBuilder resultText = new StringBuilder();
        while ((index = text.IndexOf(substring, index, StringComparison.Ordinal)) != -1) // тут способ сравнения чисто ide подсказала
        {
            count++;
            resultText.Append(text.Substring(0, index));
            resultText.Append(new string('#', substring.Length));
            text = text.Substring(index + substring.Length);
            index = 0;
        }

        resultText.Append(text);
        text = resultText.ToString();
        return count;
    }

    private static OperatorInfo[] CountWordOperators(ref string text)
    {
        int ifCount = CountOccurrences(ref text, "if");
        int elseCount = CountOccurrences(ref text, "else");
        int doCount = CountOccurrences(ref text, "do");
        int whileCount = CountOccurrences(ref text, "while");
        int matchCount = CountOccurrences(ref text, "match");
        return
        [
            new OperatorInfo() { count = ifCount - elseCount, name = "if"},
            new OperatorInfo() { count = elseCount, name = "if..else"},
            new OperatorInfo() { count = whileCount - doCount, name = "while"},
            new OperatorInfo() { count = doCount, name = "do..while"},
            new OperatorInfo() { count = matchCount, name = "match..case"}
        ];
    }

    private static OperatorInfo[] CountBraсes(ref string text, int functionsCount)
    {
        int figureBraсeCount = CountOccurrences(ref text, "{");
        int roundBraсeWithFunctionsCount = CountOccurrences(ref text, "(");
        return
        [
            new OperatorInfo() { count = figureBraсeCount, name = "{..}" },
            new OperatorInfo() { count = roundBraсeWithFunctionsCount - functionsCount, name = "(..)" },
        ];
    }

    private static Dictionary<string, int> CountFunctions(ref string text)
    {
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
                }

                if (leftShift != 0)
                {
                    i += leftShift + 1; // +2, чтобы скипнуть "(", которая оказалась не функцией, и идти дальше 
                    leftShift = 0;
                }
            }
            i++;
        }
        
        return functions;
    }
    
    public static void CountOperators(string text)
    {
        text = CleanText(text);
        foreach (var oper in NonСontroversialOperators)
        {
            Console.WriteLine(oper + ": " + CountOccurrences(ref text, oper));
        }
        foreach (var oper in СontroversialOperators)
        {
            Console.WriteLine(oper + ": " + CountOccurrences(ref text, oper));
        }
        foreach (var oper in VeryСontroversialOperators)
        {
            Console.WriteLine(oper + ": " + CountOccurrences(ref text, oper));
        }
        OperatorInfo[] wordOperators = CountWordOperators(ref text);
        foreach (var wordOper in wordOperators)
        {
            Console.WriteLine(wordOper.name + ": " + wordOper.count);
        }

        var functions = CountFunctions(ref text);
        foreach (var function in functions)
        {
            Console.WriteLine(function.Key+ ": " + function.Value);
        }
        
        wordOperators = CountBraсes(ref text, functions.Count); // мы передаем кол-во функций, чтобы от кол-ва всех пар круглых скобок отнять те, что принадлежат функциям
        foreach (var wordOper in wordOperators)
        {
            Console.WriteLine(wordOper.name + ": " + wordOper.count);
        }
    }
}