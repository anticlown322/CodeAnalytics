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
    
    
    public IEnumerable<Metrika> GetOperatorsSet()
    {
        OperatorFinder operatorFinder = new OperatorFinder();
        var operators = operatorFinder.GetOperators(_cleanText);
        return operators.Where(o => o.Count > 0);
    }
    
    public IEnumerable<Metrika> GetOperandsSet()
    {
        OperandFinder operandFinder = new OperandFinder();
        var operands = operandFinder.GetOperands(_virginText);
        return operands.Where(o => o.Count > 0);
    }
    
    
    private void PrintOperators()
    {
        OperatorFinder operatorFinder = new OperatorFinder();
        var operators = operatorFinder.GetOperators(_cleanText);
        foreach (var oper in operators.Where(oper => oper.Count > 0))
        {
            Console.WriteLine(oper.Name+ ": " + oper.Count);
        }
    }
    
    private void PrintOperands()
    {
        OperandFinder operandFinder = new OperandFinder();
        var operands = operandFinder.GetOperands(_virginText);
        foreach (var oper in operands.Where(oper => oper.Count > 0))
        {
            Console.WriteLine(oper.Name+ ": " + oper.Count);
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