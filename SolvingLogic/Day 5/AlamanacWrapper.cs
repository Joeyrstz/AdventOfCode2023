namespace SolvingLogic.Day_5;

public class AlamanacWrapper
{
    public AlamanacWrapper(string name)
    {
        Name = name;
    }
    public string Name { get; init; }
    public List<AlmanacMap> Maps { get; set; } = [];
}