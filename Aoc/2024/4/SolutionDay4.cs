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
                if (SearchHorizontal(row, col, word)) totalCount++;
                if (SearchVertical(row, col, word)) totalCount++;
                if (SearchDiagonalDownRight(row, col, word)) totalCount++;
                if (SearchDiagonalDownLeft(row, col, word)) totalCount++;
                if (SearchHorizontalBack(row, col, word)) totalCount++;
                if (SearchVerticalBack(row, col, word)) totalCount++;
                if (SearchDiagonalUpLeft(row, col, word)) totalCount++;
                if (SearchDiagonalUpRight(row, col, word)) totalCount++;
            }
        }
        return totalCount;
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
    private bool SearchHorizontal(int startRow, int startCol, string word)
    {
        if (startCol + word.Length > _data[startRow].Length) return false; 
        return !word.Where((t, i) => _data[startRow][startCol + i] != t).Any();
    }
    private bool SearchVertical(int startRow, int startCol, string word)
    {
        if (startRow + word.Length > _data.Length) return false;
        return !word.Where((t, i) => _data[startRow + i][startCol] != t).Any();
    }
    private bool SearchDiagonalDownRight(int startRow, int startCol, string word)
    {
        if (startRow + word.Length > _data.Length || startCol + word.Length > _data[startRow].Length) return false;
        return !word.Where((t, i) => _data[startRow + i][startCol + i] != t).Any();
    }
    private bool SearchDiagonalDownLeft(int startRow, int startCol, string word)
    {
        if (startRow + word.Length > _data.Length || startCol - word.Length < -1) return false;
        return !word.Where((t, i) => _data[startRow + i][startCol - i] != t).Any();
    }
    private bool SearchHorizontalBack(int startRow, int startCol, string word)
    {
        if (startCol - word.Length < -1) return false;
        return !word.Where((t, i) => _data[startRow][startCol - i] != t).Any();
    }
    private bool SearchVerticalBack(int startRow, int startCol, string word)
    {
        if (startRow - word.Length < -1) return false;
        return !word.Where((t, i) => _data[startRow - i][startCol] != t).Any();
    }
    private bool SearchDiagonalUpLeft(int startRow, int startCol, string word)
    {
        if (startRow - word.Length < -1 || startCol - word.Length < -1) return false;
        return !word.Where((t, i) => _data[startRow - i][startCol - i] != t).Any();
    }
    private bool SearchDiagonalUpRight(int startRow, int startCol, string word)
    {
        if (startRow - word.Length < -1 || startCol + word.Length > _data[startRow].Length) return false;
        return !word.Where((t, i) => _data[startRow - i][startCol + i] != t).Any();
    }
}