namespace Day3;

public class RucksackOrganizer
{
    private IPrioritySetter _priority;
    public RucksackOrganizer( IPrioritySetter priority)
    {
        _priority = priority;
    }
    public int Organize(string path)
    {
        int result1 = 0;
        int result2 = 0;

        var splitted = File.ReadAllLines(path);
        
        foreach (string sac in splitted)
        {
            result1 += _priority.GetPriority( GetDuplicates(sac));
        }
        
        for (int i = 0; i < splitted.Length -2; i+=3) 
        {
            var common = splitted[i].Intersect(splitted[i+1].Intersect(splitted[i+2]));
            result2 += _priority.GetPriority(common.First());
        }
        return result1;
    }

    public char GetDuplicates(string compartments)
    {
        var half = compartments.Substring(0, (compartments.Length / 2));
        var half2 = compartments.Substring((compartments.Length / 2), (compartments.Length / 2));
        
        var common = half.Intersect(half2);
        return common.First();
    }

}