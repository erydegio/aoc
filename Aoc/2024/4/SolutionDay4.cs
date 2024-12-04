using Aoc2024;

namespace Aoc._2024._4;

public class SolutionDay4
{
    private readonly char[][] _data = Utils.CreateGrid("../../../4/input.txt");

    public int CountOccurrencesPart1(string word)
    {
        int totalCount = 0;
        for (int row = 0; row < _data.Length; row++)
        {
            for (int col = 0; col < _data[row].Length; col++)
            {
                totalCount += SearchInAllDirections(row, col, word);
            }
        }
        return totalCount;
    }

    private int SearchInAllDirections(int row, int col, string word)
    {
        int count = 0;
        count += Search(row, col, word, 0, 1); // Horizontal
        count += Search(row, col, word, 1, 0); // Vertical
        count += Search(row, col, word, 1, 1); // Diagonal Down Right
        count += Search(row, col, word, 1, -1); // Diagonal Down Left
        count += Search(row, col, word, 0, -1); // Horizontal Back
        count += Search(row, col, word, -1, 0); // Vertical Back
        count += Search(row, col, word, -1, -1); // Diagonal Up Left
        count += Search(row, col, word, -1, 1); // Diagonal Up Right
        return count;
    }

    private int Search(int startRow, int startCol, string word, int rowIncrement, int colIncrement)
    {
        for (int i = 0; i < word.Length; i++)
        {
            int newRow = startRow + i * rowIncrement;
            int newCol = startCol + i * colIncrement;
            if (!IsValidPosition(newRow, newCol) || _data[newRow][newCol] != word[i]) return 0;
        }
        return 1;
    }

    public int CountOccurrencesPart2()
    {
        int totalCount = 0;
        for (int row = 0; row < _data.Length; row++)
        {
            for (int col = 0; col < _data[row].Length; col++)
            {
                if (_data[row][col] != 'A') continue;
                int count = 0;
                
                if (!IsValidPosition(row - 1, col - 1) || !IsValidPosition(row + 1, col + 1)) continue;
                if ((_data[row - 1][col - 1] == 'M' && _data[row + 1][col + 1] == 'S') ||
                    (_data[row - 1][col - 1] == 'S' && _data[row + 1][col + 1] == 'M'))
                    count++;

                if (IsValidPosition(row + 1, col - 1) && IsValidPosition(row - 1, col + 1))
                {
                    if ((_data[row + 1][col - 1] == 'M' && _data[row - 1][col + 1] == 'S') ||
                        (_data[row + 1][col - 1] == 'S' && _data[row - 1][col + 1] == 'M'))
                        count++;
                }
                if (count == 2) totalCount++;
            }
        }
        return totalCount;
    }

    private bool IsValidPosition(int row, int col) => row >= 0 && row < _data.Length && col >= 0 && col < _data[row].Length; 
}