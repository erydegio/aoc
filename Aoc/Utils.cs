namespace Aoc2024;

public class Utils
{
    public static List<string> ReadLines(string path) {

        var res = File.ReadAllText(path).Split("\n", StringSplitOptions.TrimEntries).ToList();
        return res;
    }

    public static char[][] CreateGrid(string path)
    {
            var lines = File.ReadAllLines(path);
            char[][] grid = new char[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
                grid[i] = lines[i].ToCharArray();

            return grid;
    }

     public static Dictionary<char, List<(int, int)>> GetNodesPoints(string[] lines)
     {
        Dictionary<char, List<(int, int)>> points = new Dictionary<char, List<(int, int)>>();

        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                char point = lines[y][x];
                if (point != '.')
                {
                    if (!points.ContainsKey(point))
                    {
                        points[point] = new List<(int, int)>();
                    }
                    points[point].Add((x, y));
                }
            }
        }
        return points;
    }
}