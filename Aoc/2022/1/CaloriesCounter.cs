using System.Collections.Immutable;
using Tests;
namespace Day1;

public class CaloriesCounter : ICounter
{
    public int GetData(string path)
    {
        string[] elfs = File.ReadAllText(path).Split("\n\n");
        var res = elfs.Select(GetCaloriesOneElf);
        
        return res.OrderBy(x => -x).ToList().Take(3).Sum();
    }

    public int GetCaloriesOneElf(string calories)
    {
        return calories.Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse).Sum();
    }
}