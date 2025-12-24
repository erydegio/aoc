namespace Aoc._2025._7;

public static class Laboratories
{
    public static long Run1()
    {
        var d = Utils.ReadLines("example.txt");
        var splits = 0;
        HashSet<int> beams = [d[0].IndexOf('S')];

        foreach (var line in d.Skip(1))
        {
            if (line.All(c => c == '.') ) continue;
            
            var splitters = FindSplitters(line);
            HashSet<int> commonPositions = beams.Intersect(splitters).ToHashSet();
            
            if (commonPositions.Count == 0) continue;
            
            splits += commonPositions.Count;
            beams.ExceptWith(commonPositions);
            
            var newBeams = commonPositions
                .SelectMany(b => new[]{b - 1, b + 1})
                .Where(b => b >= 0)
                .ToHashSet();
            
            beams.UnionWith(newBeams);
        }
        return splits;
    }

    public static long Run2()
    {
        var d = Utils.ReadLines("example.txt");
        var beams = new Dictionary<int, long>
        {
            [d[0].IndexOf('S')] = 1
        };

        foreach (var line in d.Skip(1))
        {
            if (line.All(c => c == '.') ) continue;
            var splitters = FindSplitters(line);
            
            var commonPositions = beams.Keys.Where(b => splitters.Contains(b)).ToList();
            if (commonPositions.Count == 0) continue;
            
            var newBeams = new Dictionary<int, long>();
            foreach (var kvp in beams.Where(kvp => !commonPositions.Contains(kvp.Key)))
            {
                newBeams[kvp.Key] = kvp.Value;
            }
            
            foreach (var pos in commonPositions)
            {
                long pathCount = beams[pos]; 
                int leftPos = pos - 1;
                int rightPos = pos + 1;
            
                if (leftPos >= 0)
                {
                    newBeams.TryAdd(leftPos, 0);
                    newBeams[leftPos] += pathCount;
                }

                if (rightPos >= d[0].Length) continue;
                
                newBeams.TryAdd(rightPos, 0);
                newBeams[rightPos] += pathCount;  
            }
            beams = newBeams;
        }
        return beams.Values.Sum();
    }

    private static HashSet<int> FindSplitters(string line)
    {
        HashSet<int> splitters = [];
        int index = line.IndexOf('^');
            
        while (index != -1) {
            splitters.Add(index);
            index = line.IndexOf('^', index + 1);
        }
        return splitters;
    }
}