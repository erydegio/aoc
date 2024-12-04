namespace Day6;

public class ComunicationSystem
{
    public int Response1(string path)
    {
        string[] data = File.ReadAllText(path).Split("\n");
        var marker = FindMarker(data[0]);
        var res = ReportValue(data[0], marker);
        return res;
    }
    public int ReportValue(string data, string marker)
    {
        var position = data.IndexOf(marker, StringComparison.Ordinal)+14;
        return position;
    }

    public string FindMarker(string data)
    {
        var temp = "";
        foreach (char letter in data)
        {
            if (temp.Contains(letter))
            {
                //temp con letter tagliato via + letteranuova

                var a = temp.IndexOf(letter) +1 ;
                var substring = temp.Substring(a, temp.Length - a);
                temp = ""+substring+letter;
            }
            else
            {
                temp += letter; 
                
            }
            
            if (temp.Length == 14) break;
        }
        return temp;
    }
}