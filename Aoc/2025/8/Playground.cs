namespace Aoc._2025._8;

public static class Playground
{
    public static long Run1()
    {
        const int connectionsToMake = 1000;
        var points = Read3DPoints();
        var edges = CalcDistances(points);
        var uf = new UnionFind(points.Count);
        
        edges.Sort((a, b) => a.Dist2.CompareTo(b.Dist2));
        
        for (var k = 0; k < connectionsToMake; k++)
        {
            uf.Union(edges[k].A, edges[k].B);
        }

        var sizes = uf.GetCircuitSizes();
        return (long)sizes[0] * sizes[1] * sizes[2];
    }

    public static long Run2()
    {
        const int connectionsToMake = 1000;
        var points = Read3DPoints();
        var edges = CalcDistances(points);
        var uf = new UnionFind(points.Count);
        long result = 0;

        edges.Sort((a, b) => a.Dist2.CompareTo(b.Dist2));

        for (var k = 0; k < connectionsToMake; k++)
        {
            uf.Union(edges[k].A, edges[k].B);
        }

        foreach (var (lastA, lastB, _) in edges)
        {
            int rootA = uf.Find(lastA);
            int rootB = uf.Find(lastB);

            if (rootA == rootB) continue; 
            
            uf.Union(lastA, lastB);

            if (uf.CountCircuits() != 1) continue;
            
            result = (long)points[lastA].X * points[lastB].X;
            break;
        }
        return result;
    }

    private static List<Edge> CalcDistances(List<(int X, int Y, int Z)> points)
    {
        List<Edge> edges = [];

        for(int i = 0; i < points.Count; i++)
        {
            for(int j = i+1; j < points.Count; j++)
            {
                long dx = points[i].X - points[j].X;
                long dy = points[i].Y - points[j].Y;
                long dz = points[i].Z - points[j].Z;
        
                long dist2 = dx*dx + dy*dy + dz*dz;
                edges.Add(new Edge(i, j, dist2));
            }
        }
        return edges;
    }

    private static List<(int X, int Y, int Z)> Read3DPoints()
    {
        List<(int X, int Y, int Z)> points = [];

        foreach (var line in Utils.ReadLines("example.txt"))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var parts = line.Split(',');
            points.Add((
                int.Parse(parts[0]),
                int.Parse(parts[1]),
                int.Parse(parts[2])
            ));
        }
        return points;
    }
}

class UnionFind(int n)
{
    private int[] parent = Enumerable.Range(0, n).ToArray();
    private int[] size = Enumerable.Repeat(1, n).ToArray();

    public int Find(int x)
    {
        if (parent[x] != x) parent[x] = Find(parent[x]); 
        return parent[x];
    }

    public void Union(int x, int y)
    {
        int rootX = Find(x);
        int rootY = Find(y);

        if (rootX == rootY) return; 

        if (size[rootX] < size[rootY])
            (rootX, rootY) = (rootY, rootX);

        parent[rootY] = rootX;
        size[rootX] += size[rootY];
    }

    public List<int> GetCircuitSizes()
    {
        var circuits = new Dictionary<int, int>();
        for (var i = 0; i < parent.Length; i++)
        {
            int root = Find(i);
            circuits.TryAdd(root, 0);
            circuits[root]++;
        }
        return circuits.Values.OrderByDescending(x => x).ToList();
    }
    
    public int CountCircuits()
    {
        var roots = new HashSet<int>();
        for (var i = 0; i < parent.Length; i++)
        {
            roots.Add(Find(i));
        }
        return roots.Count;
    }
}

public record Edge(int A, int B, long Dist2);
