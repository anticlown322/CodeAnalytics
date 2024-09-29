namespace CoreTypes.Classes;
public class Metrika
{
    public Metrika(string name)
    {
        Name = name;
    }
    public int Count { get; set; }
    public string Name { get; private set; }
}