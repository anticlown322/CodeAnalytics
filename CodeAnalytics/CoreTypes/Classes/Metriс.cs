namespace CoreTypes.Classes;
public class Metriс(string name)
{
    public int Index { get; set; }
    public string Name { get; private set; } = name;
    public int Count { get; set; }
}