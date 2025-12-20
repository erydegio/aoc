namespace Aoc._2025._6;

public static class TrashCompactor
{
    public static long Run1()
    {
        var data = 
            Utils.ReadLines("/example.txt")
            .Select(s => s.Split(" ", StringSplitOptions.TrimEntries).Where(s2 => !string.IsNullOrEmpty(s2)).ToList()).ToList();
        var lines = data.Count - 1;
        var columnsCount = data.First().ToList().Count;
        var operands = data.Last().ToList();
        
        List<long> results = data.First().Select(long.Parse).ToList();

        for (var i = 1; i <= lines -1; i++)
        {
            for (var j = 0; j <= columnsCount -1; j++)
            {
                results[j] = Calculate(operands[j], results[j], long.Parse(data[i][j]));
            }
        }
        return results.Sum();
    }

    public static long Run2()
    {
        var data = ReadProblems(Utils.CreateGrid("example.txt"));
        List<long> results = [];

        foreach (var d in data)
        {
            var operand = d.Last();
            d.RemoveAt(d.Count - 1);

            results.Add(operand == "+"
                ? d.Aggregate(0L, (a, b) => a + long.Parse(b))
                : d.Aggregate(1L, (a, b) => a * long.Parse(b)));
        }
        return results.Sum();
    }
    

    private static List<List<string>> ReadProblems(char[][] data)
    {
        var lines = data.ToList().Count - 1; 
        var columnsIndex = data.First().ToList().Count -1;
        var operands = data.Last().Where(c => !char.IsWhiteSpace(c));
        var stack = new Stack<char>(operands);
        
        List<List<string>> results = [];
        List<string> tempResults = new List<string>();
        
        for (var i = columnsIndex; i >= 0;  i--)
        {
            var digit = "";

            for (int j = 0; j <= lines; j++)
            {
                var read = data[j][i];
                if (char.IsDigit(read)) digit += read;

                if (j != lines) continue; 
                if (read == ' ')
                {
                    if (string.IsNullOrEmpty(digit.Trim())) continue; 
                    
                    tempResults.Add(digit.Trim());
                    digit = "";
                }
                else
                {
                    tempResults.Add(digit.Trim());
                    digit = "";
                    
                    tempResults.Add(stack.Pop().ToString());
                    results.Add(tempResults.ToList());
                    tempResults.Clear(); 
                }
            }
        }
        return results;
    }

    private static long Calculate(string operand, long from, long toAdd)
        => operand == "+" ? from + toAdd : (from * toAdd);
} 