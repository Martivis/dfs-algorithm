var edges = new List<(int, int)>();
Console.WriteLine("Введите рёбра графа (например: 1 2). Для завершения введите пустую строку:");

while (true)
{
    var line = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(line)) 
        break;

    var parts = line.Split();
    if (int.TryParse(parts[0], out int u) && int.TryParse(parts[1], out int v))
    {
        edges.Add((u, v));
    }
}

Console.Write("Введите стартовую вершину: ");
int startVertex;
if (!int.TryParse(Console.ReadLine(), out startVertex))
{
    return;
}

Console.Write("Введите конечную вершину (b): ");
int endVertex;
if (!int.TryParse(Console.ReadLine(), out endVertex))
{
    return;
}

var (path, length) = DFSFindPath(edges, startVertex, endVertex);

if (path.Count > 0)
{
    Console.WriteLine("Путь от {0} до {1}: {2}", startVertex, endVertex, string.Join(" -> ", path));
    Console.WriteLine("Длина пути: " + length);
}
else
{
    Console.WriteLine("Путь от {0} до {1} не найден.", startVertex, endVertex);
}


static (List<int> path, int length) DFSFindPath(List<(int, int)> edges, int start, int target)
{
    var graph = new Dictionary<int, List<int>>();
    foreach (var (u, v) in edges)
    {
        if (!graph.ContainsKey(u)) graph[u] = new List<int>();
        if (!graph.ContainsKey(v)) graph[v] = new List<int>();
        graph[u].Add(v);
        graph[v].Add(u);
    }

    var visited = new HashSet<int>();
    var path = new List<int>();
    bool found = false;

    void Dfs(int current, List<int> currentPath)
    {
        if (found || visited.Contains(current)) return;

        visited.Add(current);
        currentPath.Add(current);

        if (current == target)
        {
            path.AddRange(currentPath);
            found = true;
            return;
        }

        if (graph.ContainsKey(current))
        {
            foreach (var neighbor in graph[current])
            {
                Dfs(neighbor, new List<int>(currentPath));
            }
        }
    }

    Dfs(start, new List<int>());
    return (path, path.Count > 0 ? path.Count - 1 : -1);
}