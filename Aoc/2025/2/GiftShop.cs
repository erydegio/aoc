namespace Aoc._2025._2;

public static class GiftShop
{
    public static long Run1()
        => Solve("input.txt", GetInvalid);

    public static long Run2()
        => Solve("input.txt", CountInvalid);

    private static long Solve(string filePath, Func<long, long> validator)
    {
        var ranges = File.ReadAllText(filePath)
            .Split(",", StringSplitOptions.TrimEntries);
        long result = 0;

        foreach (var range in ranges)
        {
            var dashIndex = range.IndexOf('-');
            var min = long.Parse(range.AsSpan(0, dashIndex));
            var max = long.Parse(range.AsSpan(dashIndex + 1));

            for (long i = min; i <= max; i++) result += validator(i);
        }
        return result;
    }

    private static long CountInvalid(long number)
    {
        var numberAsString = number.ToString();
        long totalLength = numberAsString.Length;

        for (int sequenceLength = 1; sequenceLength <= totalLength / 2; sequenceLength++)
        {
            if (totalLength % sequenceLength != 0) continue;

            if (IsRepeatedSequence(numberAsString, sequenceLength))
                return number;
        }
        return 0;
    }

    private static bool IsRepeatedSequence(string text, int sequenceLength)
    {
        for (int i = sequenceLength; i < text.Length; i++)
        {
            if (text[i] != text[i % sequenceLength]) return false;
        }
        return true;
    }

    private static long GetInvalid(long number)
    {
        var numberAsString = number.ToString();
        if (numberAsString.Length % 2 != 0) return 0;
        int half = numberAsString.Length / 2;
        
        return numberAsString
            .AsSpan(0, half).SequenceEqual(numberAsString.AsSpan(half))
            ? number
            : 0;
    }
}