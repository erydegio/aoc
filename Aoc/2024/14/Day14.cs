using Aoc2024;

namespace Aoc._2024._14;

public class Day14(int xLimit, int yLimit)
{
    private List<Robot> _robots = ReadFile();
    private int XLimit { get; } = xLimit;
    private int YLimit { get; } = yLimit;

    public int Part1(int moves)
    {
        List<Robot> newRobots = _robots;
        for (int i = 0; i < moves; i++)
        {
            newRobots = newRobots.Select(Move).ToList();
        }

        var middleX = (XLimit + 1) / 2;
        var middleY = (YLimit + 1) / 2;

        var relevantRobots = newRobots.Where(r => r.X != middleX && r.Y != middleY).ToList();

        var q1 = relevantRobots.Count(r => r.X >= 0 && r.X <= middleX - 1 && r.Y >= 0 && r.Y <= middleY - 1);
        var q2 = relevantRobots.Count(r => r.X >= middleX + 1 && r.X <= XLimit && r.Y >= 0 && r.Y <= middleY - 1);
        var q3 = relevantRobots.Count(r => r.X >= 0 && r.X <= middleX - 1 && r.Y <= YLimit && r.Y >= middleY + 1);
        var q4 = relevantRobots.Count(r => r.X >= middleX + 1 && r.X <= XLimit && r.Y <= YLimit && r.Y >= middleY + 1);

        return q1 * q2 * q3 * q4;
    }

    public int Part2(int secondLimit, int continuousLimit)
    {
        var seconds = 0;

        List<Robot> newRobots = _robots;
        while (seconds < secondLimit)
        {
            var continuous = 0;
            newRobots = newRobots.Select(Move).ToList();
            seconds++;

            foreach (var robot in newRobots)
            {
                if (newRobots.Any(r => r.X == robot.X && r.Y == robot.Y - 1) &&
                    newRobots.Any(r => r.X == robot.X && r.Y == robot.Y + 1) &&
                    newRobots.Any(r => r.X + 1 == robot.X && r.Y == robot.Y) &&
                    newRobots.Any(r => r.X == robot.X -1 && r.Y == robot.Y))
                {
                    continuous++;
                    Console.WriteLine($"continuous: {continuous}");
                }
            }
   
            if (continuous > continuousLimit) break;
        }
        return seconds;
    }

    private Robot Move(Robot robot)
    {
        return robot with
        {
            X = Utils.Module(robot.X + robot.Vx, 0,XLimit),
            Y = Utils.Module(robot.Y + robot.Vy, 0, YLimit)
        };
    }
    
    public static List<Robot> ReadFile() 
    { 
        List<Robot> robots = new List<Robot>();
        List<string> lines = Utils.ReadLines("../../../Day14/example.txt");
        
        foreach (var line in lines)
        {
            string[] parts = line.Split(' '); 
            string[] pValues = parts[0].Substring(2).Split(','); 
            string[] vValues = parts[1].Substring(2).Split(','); 
            robots.Add(new Robot(int.Parse(pValues[0]), int.Parse(pValues[1]), int.Parse(vValues[0]), int.Parse(vValues[1]))); 
        } 
        return robots; 
    }
}

public readonly record struct Robot(int X, int Y, int Vx, int Vy);