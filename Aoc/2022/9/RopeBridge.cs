namespace Day9;

public class RopeBridge
{
    public Element H = new();
    public Element T = new();
    HashSet<Position> TPositions = new();

    public RopeBridge()
    {
        AddPosition();
    }

    public void Response1(string path)
    {
        var data = File.ReadAllText(path).Split("\n", StringSplitOptions.RemoveEmptyEntries);
        
        foreach (var motion in data)
        {
            Move(motion, H, T);
        }
        Console.WriteLine(TPositions.Count);
    }

    public void Response2(string path)
    {
        var data = File.ReadAllText(path).Split("\n", StringSplitOptions.RemoveEmptyEntries);
        var tails = new List<Element>();
        HashSet<Position> T9Positions = new();

        for (var i = 0; i < 10; i++)
        {
            tails.Add(new Element());
        }
        
        var T9 = tails.Last();
        var H = tails.First();
        T9Positions.Add(T9.GetPosition());
        
        foreach (var motion in data)
        {
            var parts = motion.Split();
            var letter = parts[0];
            var mov = int.Parse(parts[1]);
            
            for (var i = 0; i < mov; i++)
            {
                MoveH(letter, H);
                for (var y=0; y < tails.Count -1; y++)
                {
                    MoveT(tails[y], tails[y+1]);
                    T9Positions.Add(T9.GetPosition());
                }
            }
        }
        Console.WriteLine(T9Positions.Count);
    }


    public void Move(string motion, Element H, Element T)
    {
        var parts = motion.Split();
        var letter = parts[0];
        var mov = int.Parse(parts[1]);
        for (var i = 0; i < mov; i++)
        {
            MoveH(letter, H);
            MoveT(H,T);
            AddPosition();
        }
    }

    private void MoveH(string letter, Element H)
    {
        switch (letter)
        {
            case "U":
                H.MoveUp();
                break;
            case "D":
                H.MoveDown();
                break;
            case "L":
                H.MoveLeft();
                break;
            case "R":
                H.MoveRight();
                break;
        }
    }

    private void MoveT(Element H, Element T)
    {
        var hx = H.GetPosition().X;
        var hy = H.GetPosition().Y;
        var tx = T.GetPosition().X;
        var ty = T.GetPosition().Y;
        var diffY = (uint)Math.Abs(hy - ty);
        var diffX = (uint)Math.Abs(hx - tx);

        if (hx == tx && diffY > 1)
        {
            if (hy > ty) T.MoveUp();
            else if (hy < ty) T.MoveDown();
        }
        else if (hy == ty && diffX > 1)
        {
            if (hx > tx) T.MoveRight();
            else if (hx < tx) T.MoveLeft();
        }
        else if (hx != tx && hy != ty && (diffX > 1 || diffY > 1))
        {
            if (tx > hx && ty < hy)
            {
                T.MoveUp();
                T.MoveLeft();
            }
            else if (tx < hx && ty < hy)
            {
                T.MoveUp();
                T.MoveRight();
            }
            else if (tx > hx && ty > hy)
            {
                T.MoveDown();
                T.MoveLeft();
            }
            else if (tx < hx && ty > hy)
            {
                T.MoveDown();
                T.MoveRight();
            }
        }
    }

    private void AddPosition()
    {
        if (TPositions.Add(T.GetPosition()))
        {
            Console.WriteLine("new position added");
        }
    }
}

public record Position(int X, int Y);

public class Element
{
    private Position Position;

    public Element()
    {
        Position = new Position(0, 0);
    }

    public Position GetPosition()
    {
        return Position;
    }

    public int GetX()
    {
        return Position.X;
    }

    public int GetY()
    {
        return Position.Y;
    }

    public void MoveLeft()
    {
        Position = Position with { X = Position.X - 1 };
    }

    public void MoveUp()
    {
        Position = Position with { Y = Position.Y + 1 };
    }

    public void MoveDown()
    {
        Position = Position with { Y = Position.Y - 1 };
    }

    public void MoveRight()
    {
        Position = Position with { X = Position.X + 1 };
    }
}