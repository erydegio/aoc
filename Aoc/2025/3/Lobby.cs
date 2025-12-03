namespace Aoc._2025._3;

public static class Lobby
{
    public static long Run1()
    {
        var data = Utils.ReadLines("../../../2025Day3/example.txt");
        return data.Sum(line => long.Parse(FindMaxTwoDigits(line)));
    }
    
    public static long Run2()
    {
        var data = Utils.ReadLines("../../../2025Day3/input.txt");
        return data.Sum(line => long.Parse(FindLargestNumber(line, 12)));
    }

    private static string FindMaxTwoDigits(string numbers)
    {
        return Enumerable.Range(0, numbers.Length - 1)
            .SelectMany(i => Enumerable.Range(i + 1, numbers.Length - i - 1)
                .Select(j => new
                {
                    Value = (numbers[i] - '0') * 10 + (numbers[j] - '0'),
                    Pair = $"{numbers[i]}{numbers[j]}"
                }))
            .OrderByDescending(x => x.Value)
            .First()
            .Pair;
    }
    
    private static string FindLargestNumber(string numbers, int targetLength)
    {
        var toRemove = numbers.Length - targetLength;
        var result = new char[numbers.Length];
        var writeIndex = 0;
    
        foreach (var digit in numbers)
        {
            while (writeIndex > 0 && result[writeIndex - 1] < digit && toRemove > 0)
            {
                writeIndex--;
                toRemove--;
            }
            result[writeIndex++] = digit;
        }
        var finalLength = writeIndex - toRemove;
        return new string(result.AsSpan(0, finalLength));
    }
}