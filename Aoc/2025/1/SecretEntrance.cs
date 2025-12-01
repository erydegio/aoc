namespace Aoc._2025._1;

public static class SecretEntrance
{
    private const int Start = 0;
    private const int End = 99;
    
    public static int Run1()
    {
        var lines = File.ReadAllLines("input.txt");
        var times = 0;
        var position = 50;
        
        foreach (var line in lines)
        {
            var command = line.First();
            var steps = int.Parse(line.Substring(1));

            position = command == 'R' 
                ? Utils.Module(position + steps,  Start, End) 
                : Utils.Module(position - steps,  Start, End);

            if (position == 0) times++;
        }
        return times;
    }
     
    public static int Run2()
    {
        var lines = File.ReadAllLines("/input.txt");
        var timesTroughZero = 0;
        var position = 50;
        
        foreach (var line in lines)
        {
            var direction = line.First();
            var steps = int.Parse(line.Substring(1));
            var delta = direction == 'R' ? 1 : -1;
            
            for (var i = 1; i <= steps; i++)
            {
                position = Utils.Module(position + delta, Start, End);
                if (position == 0)
                    timesTroughZero++;
            }
        }
        return timesTroughZero;
    }
}