namespace SolvingLogic.Day_8;

public class Graph
{
    public Dictionary<string, GraphNode> Nodes { get; set; } = new();
    
    
    public GraphNode GetOrCreate(string id)
    {
        if (Nodes.TryGetValue(id, out var foundNode))
        {
            return foundNode;
        }
        else
        {
            var node = new GraphNode(id);
            Nodes.Add(id, node);
            return node;
        }
    }
}