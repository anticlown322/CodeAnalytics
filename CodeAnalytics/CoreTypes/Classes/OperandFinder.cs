using System.Text;

namespace Lexer;

public class OperandFinder
{
    private readonly string[] _operands =
    [
        "Byte", "Short", "Int", "Long", "Float", "Double", "Char", "String", "Boolean", 
        "var", "val", "object", "class", "trait"
    ];

    public Dictionary<string, int> GetOperands(string text)
    {
        var operands = new Dictionary<string, int>();
        return operands;
    }

    private List<int> FindKeyWords(ref string text, string keyWord)
    {
        //проходимя по строке и ищем, в каких местах встречатеся наще ключевое слово 
        int index = 0;
        List<int> indexes = new List<int>();
        while ((index = text.IndexOf(keyWord, index, StringComparison.Ordinal)) != -1) 
        {
            indexes.Add(index);
            index = 0;
        }
        return indexes;
    }

    private HashSet<string> FindNames(ref string text, string keyWord)
    {
        var indexes = FindKeyWords(ref text, keyWord);
        HashSet<string> names = new();
        StringBuilder name = new();
        int pos;
        foreach (var index in indexes)
        {
            if (index < text.Length - 1 && text[index + 1] == ' ')
            {
                pos = index + 2;
                while (pos < text.Length && text[pos] != ' ')
                {
                    name.Append(text[pos++]);
                }
            }
            names.Add(name.ToString());
            name.Clear();
        }
        return names;
    }

    /*private Dictionary<string, int> CountNames(ref string text, string[] operands)
    {
        foreach (var oper in operands)
        {
            var set = FindNames(ref text, oper);
        }
        //Dictionary<string, int> operands = new();
        
    }*/
    
    
    /*
     * проходимя по массиву операндов и ищем, в каких местах встречатеся наще ключевое слово (list)
     * проходимся по листу и добавляем правые слова от ключевого слова, проверяя валидность (bool), в лист (list)
     * считаем количество встреченных ключевых слов и возвращаем словарь
     */ 
    
    
}