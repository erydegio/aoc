using Aoc2024;

namespace Aoc._2024._1;

public class SolutionDay1
{
    private readonly List<int> _first = new();
    private readonly List<int> _second = new();

    public SolutionDay1() => InitFirstAndSecond();

    public int Run1()
    {
        _first.Sort(); _second.Sort();
        
        return _first.Zip(_second, (a, b) => Math.Abs(a - b)).Sum();
    }
    public long Run2()
    {
        return _first
            .Aggregate<int, long>(0, (current, n1) => current + _second.Count(n => n == n1) * n1);
    }
    private void InitFirstAndSecond()
    {
        foreach (var parts in Utils.ReadLines("../../../1/input.txt")
                     .Select(row => row.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)))
        {
            _first.Add(int.Parse(parts[0]));
            _second.Add(int.Parse(parts[1]));
        }
    }
}