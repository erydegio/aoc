namespace Day5;

public class ShipSupplies
{
    private List<ShipStack> ResList;

    public ShipSupplies(int stacksNumber)
    {
        ResList = new List<ShipStack>();
        CreateStacks(stacksNumber);
    }
    public IEnumerable<ShipStack> OrderStack(string path)
    {
        var data = File.ReadAllText(path).Split("\n\n");
        ParseStacks(data.First());
        var instructions = data.Last().Split("\n", StringSplitOptions.RemoveEmptyEntries);
        
        foreach (var info in instructions)
        {
            Execute2(ParseInstructions(info));
        }
        return ResList;
    }

    public string GetResult()
    {
        var res = "";
        foreach (var ss in ResList)
        {
            res += ss.Supplies.Peek().ToString();
        }
        return res;
    }

    public Instruction ParseInstructions(string info)
    {
        var i = info.Split(" ");
        var move =int.Parse(i[1]);
        var from = int.Parse(i[3]);
        var to = int.Parse(i[5]);

        return new Instruction(move,from,to);
    }

    public void Execute(Instruction instr)
    {
        for(int i=0; i < instr.Move; i++)
        {
            var box = ResList!.Find(s => s.Number == instr.From)!.Supplies.Pop();
            ResList.Find(s => s.Number == instr.To)!.Supplies.Push(box);
        }
    }

    public void Execute2(Instruction instr)
    {
        Stack<char> temp = new Stack<char>();
        for(int i=0; i < instr.Move; i++)
        {
            var box = ResList!.Find(s => s.Number == instr.From)!.Supplies.Pop();
            temp.Push(box);
        }
        for(int i=0; i < instr.Move; i++)
        {
            var box = temp.Pop();
            ResList.Find(s => s.Number == instr.To)!.Supplies.Push(box);
        }
    }

    private void CreateStacks(int stacksNumber)
    {
        for (var i= 1 ; i<= stacksNumber ; i++)
        {
            ResList.Add(new ShipStack{Number = i, Supplies = new Stack<char>()});
        }
    }

    public void ParseStacks(string data)
    {
        var stacks = data.Split("\n").Reverse();
        foreach (var info in stacks)
        {
            int countStack = 0; 
            for ( var i=1; i<=34; i+=4)
            {
                if (!Char.IsWhiteSpace(info[i]) && !Char.IsDigit(info[i]))
                {
                   ResList[countStack].Supplies.Push(info[i]);
                }
                countStack++;
            }
        }
    }
}

public record Instruction(int Move, int From, int To);

    

