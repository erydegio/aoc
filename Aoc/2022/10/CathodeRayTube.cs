namespace Day100;

public class CathodeRayTube
{
    public int Response1(string path)
    {
        
        var data = File.ReadAllText(path).Split("\n", StringSplitOptions.RemoveEmptyEntries);
        int cycles = 0;
        int registerX = 1;
        int signalSum = 0;

        foreach(var instruction in data){

            if (instruction == "noop"){
                cycles++;
                signalSum += calcSignalStrength(cycles, registerX);
                continue;
            }

            for (int i = 0; i < 2; i++){
                cycles++;
                signalSum += calcSignalStrength(cycles, registerX);
                if (i == 1){
                    registerX += Convert.ToInt32(instruction.Split(" ")[1]);
                }
            }
        }
        return signalSum;
    }
    
    public void Response2(string path)
    {
        //draws a single pixel during each cycle
        //be able to determine whether the sprite is visible the instant each pixel is drawn
        string monitor = "";
        int cycles = 0;
        int registerX = 0;
        int rowsize = 40;
        int colSize = 6;

        var data = File.ReadAllText(path).Split("\n", StringSplitOptions.RemoveEmptyEntries);

        foreach (var instruction in data)
        {
            if (cycles == colSize * rowsize - 1) break;
            
            if (instruction == "noop")
            {
                monitor += AddPixel(cycles, registerX);
                cycles++;
                if (cycles % rowsize == 0) monitor += "\n";
                continue;
            }

            for (int i = 0; i < 2; i++)
            {
                monitor += AddPixel(cycles, registerX);
                cycles++;
                if (cycles % rowsize == 0) monitor += "\n";
                if (i == 1)
                {
                    registerX += Convert.ToInt32(instruction.Split(" ")[1]);
                }
            }
        }
        Console.WriteLine(monitor);
    }

    private string AddPixel(int cycle, int registerX)
    {
        registerX += (cycle/40) * 40;
        int[] spritePos = { registerX, registerX + 1, registerX + 2 };
        return spritePos.Contains(cycle) ? "#" : ".";
    }

    private int calcSignalStrength(int cycles, int registerX){
        if ((cycles - 20) % 40 == 0)
        {
            return registerX * cycles;
        }
        return 0;
    }
}