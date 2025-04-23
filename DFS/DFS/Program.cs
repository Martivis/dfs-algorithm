var edges = new List<(int, int)>();
Console.WriteLine("Введите рёбра графа (например: 1 2). Для завершения введите пустую строку:");

while (true)
{
    var line = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(line)) break;

    var parts = line.Split();
    if (parts.Length == 2 && int.TryParse(parts[0], out int u) && int.TryParse(parts[1], out int v))
    {
        edges.Add((u, v));
    }
    else
    {
        Console.WriteLine("Некорректный ввод. Введите два числа, разделённых пробелом.");
    }
}

Console.Write("Введите стартовую вершину: ");
int startVertex;
while (!int.TryParse(Console.ReadLine(), out startVertex))
{
    Console.Write("Пожалуйста, введите корректное число: ");
}

var result = DFS(edges, startVertex);

Console.WriteLine("Путь обхода в глубину:");
Console.WriteLine(string.Join(", ", result));


static List<int> DFS(List<(int, int)> edges, int start)
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
    var result = new List<int>();

    void Dfs(int node)
    {
        if (visited.Contains(node)) return;

        visited.Add(node);
        result.Add(node);

        if (graph.ContainsKey(node))
        {
            foreach (var neighbor in graph[node])
            {
                Dfs(neighbor);
            }
        }
    }

    Dfs(start);
    return result;
}