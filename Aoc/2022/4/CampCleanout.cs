using System.ComponentModel.Design;

namespace Day4;

public class CampCleanOut
{
    
    public List<IEnumerable<int>> ParseLine(string text)
    {
        var splitted = text.Split(",");
        var list = new List<IEnumerable<int>>();
        
        foreach (var range in splitted)
        {
            var r = range.Split("-");
            var start = int.Parse(r.First());
            var end = int.Parse(r.Last());
            list.Add(Enumerable.Range(start, end - start + 1));
        }
        return list;
    }

    public bool IsFullContained(List<IEnumerable<int>> input)
    {
        if (!input.First().Except(input.Last()).Any() ||
            !input.Last().Except(input.First()).Any())
        {
            return true;
        }
        return false;
    }

    public bool IsOverLap(List<IEnumerable<int>> input)
    {
        var first = input.First();
        var second = input.Last();

        if (first.Intersect(second).Any()) 
        {
            return true;
        }
        return false;
    }

    public int Response1(string path)
    {
        var input = File.ReadAllLines(path);
        int count = 0;
        
        foreach (var line in input)
        {
            var pairs = ParseLine(line);
            if (IsFullContained(pairs)){
                count ++;
            }
        }
        return count;
    }
    
    public int Response2(string path)
    {
        var input = File.ReadLines(path);
        int count = 0;
        
        foreach (var line in input)
        {
            var pairs = ParseLine(line);
            if (IsOverLap(pairs)){
                count ++;
            }
        }
        return count;
    }
}
