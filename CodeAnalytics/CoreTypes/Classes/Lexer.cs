using System.Text;

namespace CoreTypes.Classes;

public class Lexer
{
    private string _cleanText;
    private string _virginText;

    public Lexer(string text)
    {
        _virginText = text;
        _cleanText = CleanText(text);
    }

    public HashSet<Metriс> GetOperatorsSet()
    {
        OperatorFinder operatorFinder = new OperatorFinder();
        var operators = operatorFinder.GetOperators(_cleanText);
        return operators.Where(o => o.Count > 0).ToHashSet();
    }

    public HashSet<Metriс> GetOperandsSet()
    {
        OperandFinder operandFinder = new OperandFinder();
        var operands = operandFinder.GetOperands(_virginText);
        return operands.Where(o => o.Count > 0).ToHashSet();
    }

    public (int, int, int) GetTextParams(HashSet<Metriс> operands, HashSet<Metriс> operators)
    {
        int nu1 = operators.Count;
        int n1 = 0;
        foreach (var oper in operators)
        {
            n1 += oper.Count;
        }

        int nu2 = operands.Count;
        int n2 = 0;
        foreach (var oper in operands)
        {
            n1 += oper.Count;
        }

        double v = (n1 + n2) * Math.Log2(nu1 + nu2);
        return (nu1 + nu2, n1 + n2, (int)v);
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