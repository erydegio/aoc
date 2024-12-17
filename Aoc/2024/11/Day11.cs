using System.Numerics;

namespace Aoc._2024._11;

public class Day11
{
    public long Part1(int times)
    {
        var lines = File.ReadAllText("../../../Day11/example.txt").Split(' ').Select(long.Parse).ToList();
        
        for (int i = 0; i < times; i++)
            lines = ApplyRules(lines);
        
        return lines.Count;
    }

    public string Part2(int times)
    {
        var numbers  = File.ReadAllText("../../../Day11/example.txt").Split(' ');

        Dictionary<string, long> stones = new();
        numbers.GroupBy(x => x).ToList().ForEach(x => stones.Add(x.Key, x.Count()));

        for (int i = 0; i < times; i++)
        {
            var blink = new Dictionary<string, long>();
            foreach (var (number, count) in stones)
                ApplyRules(number, count, blink);

            stones = blink;
        }

        return stones.Sum(n => n.Value).ToString();
    }

    private static void ApplyRules(string line, long count, Dictionary<string, long> cache)
    {
        if (line == "0")
            Add("1", count);
        
        else if (line.Length % 2 == 0)
        {
            var half = line.Length / 2;
            var left = line[..half];
            var right = line[half..].TrimStart('0');
            if (right == "") right = "0";
            
            Add(left, count);
            Add(right, count);
        }
        else
        {
            var bigNumber = BigInteger.Parse(line);
            var multiple = bigNumber * 2024;
            Add(multiple.ToString(), count);
        }

        void Add(string key, long value)
        {
            if (!cache.TryAdd(key, value)) cache[key] += value;
        }
    }
    
    private  List<long> ApplyRules(IEnumerable<long> lines)
    {
        List<long> result = [];
        foreach (var line in lines)
        {
            result.AddRange(ApplyRule(line)); 
        }
        return result;
    }
    
    private  List<long> ApplyRule(long stone)
    {
        if (stone == 0) return [1];
        
        var s = stone.ToString();
        if (s.Length % 2 != 0) return [stone * 2024];
        var midIndex = s.Length / 2;

        return [int.Parse(s[..midIndex]), int.Parse(s[midIndex..])];
    }
}