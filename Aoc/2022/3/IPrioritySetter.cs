namespace Day3;

public interface IPrioritySetter
{
    public int GetPriority(char letter);
}

public class Priority : IPrioritySetter
{
    public int GetPriority(char letter)
    {
        int[] points = Enumerable.Range(97, 122 - 97 + 1).Concat(Enumerable.Range(65, 90 - 65 + 1)).ToArray();

        var letterVal = (int)letter;
        var point =  Array.IndexOf(points, letterVal) + 1;
        return point;
    }
}