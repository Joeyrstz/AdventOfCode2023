namespace SolvingLogic.Day_4;

public class Scratchcard
{
    public int CardNumber { get; set; }
    public int[] LeftNumbers { get; set; }
    public int[] RightNumbers { get; set; }
    
    public Scratchcard(string line)
    {
        CardNumber = int.Parse(line.Split(':')[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)[1]);
        var boxes = line.Split(':')[1].Split('|');
        var left = boxes[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var right = boxes[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        LeftNumbers = left.Select(int.Parse).ToArray();
        RightNumbers = right.Select(int.Parse).ToArray();
    }
}