using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private SudokuLevelSO newLevel;

    [SerializeField]
    private bool generateLevel = false;

    // Generation values
    int[,] levelMatrix = new int[9,9];

#if UNITY_EDITOR
    private void Update()
    {
        if(generateLevel)
        {
            GenerateNewLevel();
            generateLevel = false;
        }
    }

    private void GenerateNewLevel()
    {
        Debug.Log("Start sudoku level generator!");

        // Fill grid with 0
        FillGridWithZeros();

        // Fill diagonal
        //FillDiagonal();

        Debug.Log("Grid");
        // Fill remaining
        FillRemaining(0, 0);

        PrintSudokuLevel();

        Debug.Log("Finished level generation!");
    }

    private void FillGridWithZeros()
    {
        for (int x = 0; x < 9; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                levelMatrix[x, y] = 0;
            }
        }
    }

    private void FillDiagonal()
    {
        for (int cellNum = 0; cellNum < 3; cellNum++)
        {
            FillBox(cellNum, cellNum);
        }
    }

    int counter = 0;
    private bool FillRemaining(int row, int col)
    {
        counter++;
        if (counter > 20000000)
            return false;


        if (row < 9 && col < 9)
        {
            if (levelMatrix[row, col] != 0)
            {
                
                if ((col + 1) < 9) return FillRemaining(row, col + 1); // If there still next column, go to next col in same row
                else if ((row + 1) < 9) return FillRemaining(row + 1, 0); // If there still next row, go to next row and start in column 0
                else return true;
            }
            else
            {
                List<int> availableNumbers = GetAvailableNumbers(row, col);
                if (availableNumbers.Count > 0)
                {
                    levelMatrix[row, col] = availableNumbers[Random.Range(0, availableNumbers.Count)];
                    Debug.Log($"Position [{row}][{col}]: {levelMatrix[row, col]}");

                }

                if ((col + 1) < 9)
                {
                    //Debug.Log($"Position [{row}][{col}]: {levelMatrix[row, col]}");
                    return FillRemaining(row, col + 1);
                }
                else if ((row + 1) < 9)
                {
                    //Debug.Log($"Position [{row}][{col}]: {levelMatrix[row, col]}");
                    return FillRemaining(row + 1, 0);
                }
                else return true;
            }
        }
        else return true;
    }

    // Fill cell of 3x3
    private void FillBox(int row, int column)
    {
        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        List<int> availableNumbers = new List<int>(numbers);
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                int number = GetRandomNumberInList(availableNumbers);
                levelMatrix[row + x, column + y] = number;
                availableNumbers.Remove(number);

                //Debug.Log($"Position [{row + x}][{column + y}]: {number}");
            }
        }
    }

    private void PrintSudokuLevel()
    {
        string rowString = "";

        for (int x = 0; x < 9; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                rowString += levelMatrix[x, y].ToString();
            }

            newLevel.NumberSolution[x] = rowString;
            Debug.Log($"Row {x}: {rowString}");
            rowString = "";
        }
    }

    private bool IsAvailable(int row, int col, int num)
    {
        for (int i = 0; i < 9; ++i)
        {
            if (levelMatrix[row, i] == num) return false;
            if (levelMatrix[i, col] == num) return false;
        }

        int rowStart = (row / 3) * 3;
        int colStart = (col / 3) * 3;

        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (levelMatrix[rowStart + x, colStart + y] == num) return false;
            }
        }

        return true;
    }

    private List<int> GetAvailableNumbers(int row, int col)
    {
        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        List<int> availableNumbers = new List<int>(numbers);

        for (int i = 0; i < 9; ++i)
        {
            if (availableNumbers.Contains(levelMatrix[row, i]))
                availableNumbers.Remove(levelMatrix[row, i]);
            if (availableNumbers.Contains(levelMatrix[i, col]))
                availableNumbers.Remove(levelMatrix[i, col]);
        }

        int rowStart = (row / 3) * 3;
        int colStart = (col / 3) * 3;

        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (availableNumbers.Contains(levelMatrix[rowStart + x, colStart + y]))
                    availableNumbers.Remove(levelMatrix[rowStart + x, colStart + y]);
            }
        }

        return availableNumbers;
    }

    // Gives a random element from a given list
    private int GetRandomNumberInList(List<int> list)
    {
        int idx = Random.Range(0, list.Count);
        return list[idx];
    }
#endif
}

