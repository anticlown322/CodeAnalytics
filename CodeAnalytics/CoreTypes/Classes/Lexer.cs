using System.Text;

namespace Lexer;

public class Lexer
{
    private string _cleanText;

    public Lexer(string text)
    {
        _cleanText = CleanText(text);
    }
    public void PrintOperators()
    {
        OperatorFinder operatorFinder = new OperatorFinder();
        var operators = operatorFinder.GetOperators(_cleanText);
        foreach (var oper in operators)
        {
            Console.WriteLine(oper.Key+ ": " + oper.Value);
        }
    }
    
    public void PrintOperands()
    {
        OperandFinder operandFinder = new OperandFinder();
        var operands = operandFinder.GetOperands(_cleanText);
        foreach (var oper in operands)
        {
            Console.WriteLine(oper.Key+ ": " + oper.Value);
        }
    }
    
    private string CleanText(string text)
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
}