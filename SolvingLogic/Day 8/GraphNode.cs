namespace SolvingLogic.Day_8;

public class GraphNode
{
    public string Id { get; init; }
    public GraphNode LeftNode { get; set; }
    public GraphNode RightNode { get; set; }
    public GraphNode(string id)
    {
        Id = id;
    }
    
    public GraphNode Traverse(char direction)
    {
        return direction switch
        {
            'L' => LeftNode,
            'R' => RightNode,
            _ => throw new Exception("Invalid direction")
        };
    }
}