using System.Text;

namespace CoreTypes.Classes;

public class OperandFinder
{
    private readonly string[] _operands =
    [
        "Byte", "Short", "Int", "Long", "Float", "Double", "Char", "String", "Boolean", 
        "var", "val", "object", "class", "trait"
    ];

    public HashSet<Metriс> GetOperands(string text)
    {
        var operands = CountNames(ref text, _operands);
        operands.UnionWith(CountNumbers(ref text));
        
        //добавление index для тех operands, у которых Count > 0
        int index = 1;
        foreach (var oper in operands)
        {
            if (oper.Count > 0)
                oper.Index = index++;
        }
        
        return operands;
    }

    private List<int> FindKeyWords(ref string text, string keyWord)
    {
        //проходимя по строке и ищем, в каких местах встречатеся наше ключевое слово 
        List<int> indexes = [];
        int index = text.IndexOf(keyWord, StringComparison.Ordinal);
        while (index != -1)
        {
            indexes.Add(index);
            index = text.IndexOf(keyWord, index + keyWord.Length, StringComparison.Ordinal);
        }
        return indexes;
    }

    private HashSet<string> FindNames(ref string text, List<int> indexes, string keyWord)
    {
        HashSet<string> names = [];
        StringBuilder name = new();
        
        //идем влево. Если ( или , то это параметр функции. Если нет, то идем вправо
        foreach (var index in indexes)
        {
            int pos = index - 1;
            while (pos >= 0 && Equals(text[pos], ' '))
                pos--;
            if (Equals(text[pos], ':')) // это для кейса встречи слова в параметрах метода
            {
                pos--;
                while (pos >= 0 && Equals(text[pos], ' '))
                    pos--;
                while (pos >= 0 && char.IsLetterOrDigit(text[pos]))
                {
                    name.Append(text[pos]);
                    pos--;
                }
            }
            else
            {
                pos = index + keyWord.Length;
                while (pos < text.Length && Equals(text[pos], ' '))
                    pos++;
                while (pos < text.Length && char.IsLetterOrDigit(text[pos]))
                {
                    name.Append(text[pos]);
                    pos++;
                }
            }

            if (name.Length != 0)
            {
                names.Add(name.ToString());
                name.Clear();
            }
           
        }
        return names;
    }

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

    private string[] FindNumbers(ref string text)
    {
        HashSet<string> numbers = [];
        int i = 0;
        StringBuilder number = new StringBuilder();
        while (i < text.Length)
        {
            if (char.IsDigit(text[i]))
            {
                
                while (i < text.Length && (char.IsDigit(text[i]) || text[i] == ','))
                {
                    number.Append(text[i++]);
                }

                if (!char.IsLetter(text[i]))
                {
                    numbers.Add(number.ToString());
                }

                number.Clear();
            }

            i++;
        }
        
        int[] intResult = new int[numbers.Count];
        string[] strResult = numbers.ToArray();
        for (int j = 0; j < numbers.Count; j++)
        {
            intResult[j] = int.Parse(strResult[j]);
        }

        Array.Sort(intResult, (a, b) => b.CompareTo(a)); 
        for (int j = 0; j < numbers.Count; j++)
        {
            strResult[j] = intResult[j].ToString();
        }
        return strResult;
    }
    
    
    private HashSet<Metriс> CountNames(ref string text, string[] keyWords)
    {
        HashSet<string> names = [];
        foreach (var oper in keyWords)
        {
            var indexes = FindKeyWords(ref text, oper);
            if (indexes.Count != 0)
            {
                var set = FindNames(ref text, indexes, oper);
                names.UnionWith(set);
            }
        }

        var arrONames = names.ToArray();
        Array.Sort(arrONames, (a, b) => b.Length - a.Length); 
        HashSet<Metriс> countedNames = [];
        foreach (var name in arrONames)
        {
            var countedName = CountOccurrences(ref text, name);
            countedNames.Add(countedName);
        }
        
        return countedNames;
    }

    private HashSet<Metriс> CountNumbers(ref string text)
    {
        HashSet<Metriс> countedNumbers = [];
        var numbers = FindNumbers(ref text);
        foreach (var number in numbers)
        {
            var countedNumber = CountOccurrences(ref text, number);
            countedNumbers.Add(countedNumber);
        }
        return countedNumbers;
    }
    
    
    
}