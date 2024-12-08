namespace Aoc2024.Day8;

public class SolutionDay8
{
    bool IsInBound(int[] ints, (int, int) valueTuple) =>
        ints.Contains(valueTuple.Item1) && ints.Contains(valueTuple.Item2);

    public HashSet<(int, int)> Part1(Dictionary<char, List<(int, int)>> data, int[] bounds)
    {
        var antiNodes = new HashSet<(int, int)>();

        foreach (var points in data.Values)
        {
            foreach (var (p1, p2) in GetPointsCombinations(points))
            {
                int deltaY = p1.Item1 - p2.Item1;
                int deltaX = p1.Item2 - p2.Item2;

                var antiNode1 = (p1.Item1 + deltaY, p1.Item2 + deltaX);
                var antiNode2 = (p2.Item1 - deltaY, p2.Item2 - deltaX);

                if (IsInBound(bounds, antiNode1)) antiNodes.Add(antiNode1);
                if (IsInBound(bounds, antiNode2)) antiNodes.Add(antiNode2);
            }
        }
        return antiNodes; // 357
    }

    public HashSet<(int, int)> Part2(Dictionary<char, List<(int, int)>> data, int[] bounds)
    {
        var antiNodes = new HashSet<(int, int)>();
        
        foreach (var points in data.Values)
        {
            foreach (var (p1, p2) in GetPointsCombinations(points))
            {
                int deltaY = p1.Item1 - p2.Item1;
                int deltaX = p1.Item2 - p2.Item2;
                
                var gcd = Gcd(deltaY, deltaX);
                deltaY /= gcd; deltaY /= gcd;
                
                int i = 0;
                while (true)
                {
                    var antiNode1 = (p1.Item1 + deltaY * i, p1.Item2 + deltaX * i); 
                    var antiNode2 = (p2.Item1 - deltaY * i, p2.Item2 - deltaX * i);

                    var isN1Inbound = IsInBound(bounds, antiNode1);
                    var isN2Inbound = IsInBound(bounds, antiNode2);
                    if (!isN1Inbound && !isN2Inbound) break; 

                    if (isN1Inbound) antiNodes.Add(antiNode1); 
                    if (isN2Inbound) antiNodes.Add(antiNode2); 
                    i++;
                }
            }
        }
        return antiNodes; // 1266
    }
    
    static int Gcd(int a, int b) { while (b != 0) { int temp = b; b = a % b; a = temp; } return a; }    
    
    static IEnumerable<((int, int), (int, int))> GetPointsCombinations(List<(int, int)> list)
    {
        return list.SelectMany((item, index) =>
            list.Skip(index + 1)
                .Select(nextItem => (item, nextItem)));
    }
}









        
