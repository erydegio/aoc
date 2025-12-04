namespace Aoc._2025._4;

public static class PrintingDepartment
{
    public static int Run1()
    {
        var data = Utils.CreateGrid("input.txt").Positions();
        return Filter(data).Count;
    }
    
    public static int Run2()
    {
        var positions = Utils.CreateGrid("input.txt").Positions();
        var totalRemoved = 0;

        while (true)
        {
            var toRemove = Filter(positions);
    
            if (toRemove.Count == 0) break; 
    
            foreach (var pos in toRemove)
            {
                positions.Remove(pos);
            }
    
            totalRemoved += toRemove.Count;
        }
        return totalRemoved;
    }

    private static List<(int x, int y)> Filter(HashSet<(int x, int y)> positions)
    {
        var directions = new[] { (-1,-1), (-1,0), (-1,1), (0,-1), (0,1), (1,-1), (1,0), (1,1) };
        
        return positions
            .Where(pos => directions
                .Count(d => positions.Contains((pos.x + d.Item1, pos.y + d.Item2))) < 4)
            .ToList();
    }

    private static HashSet<(int x, int y)> Positions(this char[][] data, char roll = '@')
    {
        return data
            .SelectMany((row, x) => row.Select((ch, y) => (ch, x, y)))
            .Where(t => t.ch == roll)
            .Select(t => (t.x, t.y))
            .ToHashSet();
    }
}