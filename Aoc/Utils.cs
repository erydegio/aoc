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
}