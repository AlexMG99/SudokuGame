using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelGenerator;

public class LevelGeneratorRandom : MonoBehaviourSingleton<LevelGeneratorRandom>
{
    // Generation values
    Square[] Sudoku = new Square[81];
    SudokuLevelSO newRandomLevel;

    public SudokuLevelSO GenerateNewLevel()
    {
        Debug.Log("Start sudoku level generator!");

        newRandomLevel = new SudokuLevelSO();

        GenerateGrid();

        PrintSudokuLevel();

        Debug.Log("Finished level generation!");

        return newRandomLevel;
    }

    private void GenerateGrid()
    {
        Clear();
        Square[] Squares = new Square[81]; // an arraylist of squares: see line 86
        List<int>[] Available = new List<int>[81]; // an arraylist of generic lists (nested lists)
                                                   // we use this to keep track of what numbers we can still use in what squares
        int c = 0; // use this to count the square we are up to

        for (int x = 0; x <= Available.Length - 1; x++)
        {
            Available[x] = new List<int>();
            for (int i = 1; i <= 9; i++)
                Available[x].Add(i);
        }

        while (c != 81) // we want to fill every square object with values
        {
            if (Available[c].Count != 0)
            {
                // and failed then backtrack
                int i = GetRan(0, Available[c].Count - 1);
                int z = Available[c][i];

                if (Conflicts(Squares, Item(c, z)) == false)
                {
                    // proposed number
                    Squares[c] = Item(c, z);    // this number works so we add it to the 
                                                // list of numbers
                    Available[c].RemoveAt(i); // we also remove it from its individual list
                    c += 1; // move to the next number
                }
                else
                    Available[c].RemoveAt(i);// this number conflicts so we remove it 
            }
            else
            {
                for (int y = 1; y <= 9; y++) // forget anything about the current square
                    Available[c].Add(y); // by resetting its available numbers
                Squares[c - 1] = new Square();
                c -= 1; // in the previous square
            }
        }

        // this produces the output list of squares
        for (int j = 0; j <= 80; j++)
        {
            Sudoku[j] = Squares[j];
            //Debug.Log($"Square: Index: {j} Value: {Squares[j].Value} SquareIndex: {Squares[j].Index} Row: {Squares[j].Down} Column: {Squares[j].Across} Cell: {Squares[j].Region}");
        }
    }

    private Square Item(int n, int v)
    {
        Square newSquare = new Square();
        n += 1;
        newSquare.Across = GetAcrossFromNumber(n);
        newSquare.Down = GetDownFromNumber(n);
        newSquare.Region = GetRegionFromNumber(n);
        newSquare.Value = v;
        newSquare.Index = n - 1;

        return newSquare;
    }

    private int GetAcrossFromNumber(int n)
    {
        int k = n % 9;
        if (k == 0)
            return 9;
        else
            return k;
    }

    private int GetDownFromNumber(int n)
    {
        int k = 0;
        if (GetAcrossFromNumber(n) == 9)
            k = n / 9;
        else
            k = n / 9 + 1;
        return k;
    }

    private int GetRegionFromNumber(int n)
    {
        int k = 0;
        int a = GetAcrossFromNumber(n);
        int d = GetDownFromNumber(n);

        if (1 <= a & a < 4 & 1 <= d & d < 4)
            k = 1;
        else if (4 <= a & a < 7 & 1 <= d & d < 4)
            k = 2;
        else if (7 <= a & a < 10 & 1 <= d & d < 4)
            k = 3;
        else if (1 <= a & a < 4 & 4 <= d & d < 7)
            k = 4;
        else if (4 <= a & a < 7 & 4 <= d & d < 7)
            k = 5;
        else if (7 <= a & a < 10 & 4 <= d & d < 7)
            k = 6;
        else if (1 <= a & a < 4 & 7 <= d & d < 10)
            k = 7;
        else if (4 <= a & a < 7 & 7 <= d & d < 10)
            k = 8;
        else if (7 <= a & a < 10 & 7 <= d & d < 10)
            k = 9;
        return k;
    }

    public void Clear()
    {
        Array.Clear(Sudoku, 0, Sudoku.Length);
    }

    private int GetRan(int lower, int upper)
    {
        return UnityEngine.Random.Range(lower, upper - 1);
    }

    private bool Conflicts(Square[] CurrentValues, Square test)
    {
        foreach (Square s in CurrentValues)
        {
            if ((s.Across != 0 & s.Across == test.Across) || (s.Down != 0 & s.Down == test.Down) || (s.Region != 0 & s.Region == test.Region))
            {
                if (s.Value == test.Value)
                    return true;
            }
        }

        return false;
    }

    private void PrintSudokuLevel()
    {
        string rowString = "";
        int row = 0;

        for (int i = 0; i < Sudoku.Length; i++)
        {
            Debug.Log($"Square: Index: {i} Value: {Sudoku[i].Value} SquareIndex: {Sudoku[i].Index} RowString: {row} Row: {Sudoku[i].Down} Column: {Sudoku[i].Across} Cell: {Sudoku[i].Region}");

            rowString += Sudoku[i].Value.ToString();

            if ((i + 1) % 9 == 0 && i != 0)
            {
                newRandomLevel.NumberSolution[row] = rowString;
                Debug.Log($"Row {row}: {rowString}");
                rowString = "";

                row++;
            }
        }

        newRandomLevel.HideNumbersByDifficulty();
    }
}
