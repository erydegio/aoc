namespace Aoc2024.Day2;

public class SolutionDay2
{
    private List<List<int>> _data = new();
    public SolutionDay2() => Init();
    public int Run1() => _data.Count(IsSafe);
    public int Run2() => _data.Count(IsSafePart2);

    private bool IsSafe(List<int> row)
    {
        bool increasing = true; 
        bool decreasing = true;
        
        for (int i = 1; i < row.Count; i++) 
        {
            var first = row[i];
            var second = row[i - 1];
            
            if (first > second) decreasing = false;
            if (first < second) increasing = false;
            if (Math.Abs(first - second) < 1 || Math.Abs(first - second) > 3) return false;
        }
        return increasing || decreasing;
    }
    
    private bool IsSafePart2(List<int> row)
    {
        if (IsSafe(row)) return true;
        
        //if removing a single level from an unsafe report would make it safe,
        //the report instead counts as safe
        for (int i = 0; i < row.Count; i++)
        {
            var subList = row.Take(i).Concat(row.Skip(i + 1)).ToList();
            if (IsSafe(subList)) return true;
        }
        return false;
    }
    
    private void Init()
    {
        foreach (var part in Utils.ReadLines("../../../2/input.txt")) 
            _data.Add(part.Split(" ").Select(n => int.Parse(n)).ToList());
    }
}