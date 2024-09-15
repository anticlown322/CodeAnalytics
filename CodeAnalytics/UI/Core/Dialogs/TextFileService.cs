using System.IO;

namespace UI.Core;

public class TextFileService : IFileService
{
    public string OpenFile(string filename)
    {
        string text;
        
        using (StreamReader reader = new StreamReader(filename))
        {
            text = reader.ReadToEnd();
            Console.WriteLine(text);
        }

        return text;
    }
}