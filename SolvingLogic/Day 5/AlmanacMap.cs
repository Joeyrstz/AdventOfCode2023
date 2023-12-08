namespace SolvingLogic.Day_5;

public class AlmanacMap
{
    public AlmanacMap(string line)
    {
        var boxes = line.Split(" ");
        DestiniationRangeStart = long.Parse(boxes[0]);
        SourceRangeStart = long.Parse(boxes[1]);
        Range = long.Parse(boxes[2]);
    }
    public long DestiniationRangeStart { get; set; }
    public long SourceRangeStart { get; set; }
    public long Range { get; set; }

    public long GetSourceMin()
    {
        return SourceRangeStart;
    }
    public long GetSourceMax()
    {
        return SourceRangeStart + Range - 1;
    }

    public long? DoesValueFit(long value)
    {
        if (value < GetSourceMin() || value > GetSourceMax())
        {
            return null;
        }

        return DestiniationRangeStart + (value - SourceRangeStart);
    }
    
}