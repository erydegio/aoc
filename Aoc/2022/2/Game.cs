namespace Day2;

public class Game
{
    private int score { get; set; }
    
    public int Play(string path)
    {
        //Read file and return list of key value pair
        var splitted = File.ReadAllText(path).Split("\n", StringSplitOptions.RemoveEmptyEntries);
        
        foreach (string kVal in splitted)
        {
            Enum.TryParse(kVal[2].ToString(), out Shape choice);
            int choicePoint = (int)SelectShape(kVal[0], kVal[2]);
            int playPoints = (int)OutcomeDefinition(kVal[2]);
            
            int total  = choicePoint + playPoints;
            score += total; 
        }
        return score;
    }
    
    //choice challenge1
    public Outcome Choice(char opp, char key)
    {
        switch(opp) 
        {
            case 'A': // rock
                if (key == 'Y'){ return Outcome.Win;}
                if (key == 'Z'){ return Outcome.Lost;}
                return Outcome.Draw;
                
            case 'B':
                if (key == 'Y'){ return Outcome.Draw;}
                if (key == 'Z'){ return Outcome.Win;}
                return Outcome.Lost;
            default:
                if (key == 'Y'){ return Outcome.Lost;}
                if (key == 'Z'){ return Outcome.Draw;}
                return Outcome.Win;
        }
    }
    
    // shape definition
    public Shape SelectShape(char opp, char key)
    {
        switch(opp) 
        {
            case 'A': // rock X
                if (key == 'Y'){ return Shape.X;} // pareggio
                if (key == 'Z'){ return Shape.Y;}
                return Shape.Z;
                
            case 'B': // paper Y
                if (key == 'Y'){ return Shape.Y;}
                if (key == 'Z'){ return Shape.Z;}
                return Shape.X;
            default: // scissor
                if (key == 'Y'){ return Shape.Z;}
                if (key == 'Z'){ return Shape.X;}
                return Shape.Y;
        }
    }
    
    //outcome definition
    public Outcome OutcomeDefinition(char key)
    {
        switch(key) 
        {
            case 'X': 
                return Outcome.Lost;
            case 'Y':
                return Outcome.Draw;
            default:
                return Outcome.Win;
        }
    }
}

public enum Shape 
{
    X = 1,
    Y,
    Z
}

public enum Outcome
{
    Lost = 0,
    Draw = 3,
    Win = 6
}

