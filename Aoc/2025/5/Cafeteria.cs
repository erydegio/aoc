namespace Aoc._2025._5;

public static class Cafeteria
{
    public static int Run1()
    {
        var data = Utils.ReadLines("input.txt");
        var separator = data.IndexOf("");
        var ranges = ParseRanges(separator, data);

        var count = 0;
        for (int i = separator + 1; i < data.Count; i++)
        {
            long ingredient = long.Parse(data[i]);

            foreach (var (start, end) in ranges)
            {
                if (ingredient < start || ingredient > end) continue;
                count++;
                break;
            }
        }
        return count;
    }

    public static long Run2()
    {
        var data = Utils.ReadLines("input.txt");
        var separator = data.IndexOf("");
        var ranges = ParseRanges(separator, data);
        
        ranges.Sort((a, b) => a.start.CompareTo(b.start));

        List<(long start, long end)> merged = new();
        var current = ranges[0];

        for (var i = 1; i < ranges.Count; i++)
        {
            if (ranges[i].start <= current.end + 1)
            {
                current.end = Math.Max(current.end, ranges[i].end);
            }
            else
            {
                merged.Add(current);
                current = ranges[i];
            }
        }
        merged.Add(current);

        long total = 0;
        foreach (var (start, end) in merged)
            total += end - start + 1;

        return total;
    }
    
    private static List<(long start, long end)> ParseRanges(int separator, List<string> data)
    {
        List<(long start, long end)> ranges = new();
        for (int i = 0; i < separator; i++)
        {
            var parts = data[i].Split('-');
            ranges.Add((long.Parse(parts[0]), long.Parse(parts[1])));
        }
        return ranges;
    }
}