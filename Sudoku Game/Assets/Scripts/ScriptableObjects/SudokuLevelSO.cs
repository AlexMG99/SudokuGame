using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level00", menuName = "ScriptableObjects/Level/New Level", order = 1)]
public class SudokuLevelSO : ScriptableObject
{
    public string[] NumberSolution => numberSolution;
    [SerializeField]
    private string[] numberSolution = new string[9];

    public string[] NumberHide => numberHide;
    [SerializeField]
    private string[] numberHide = new string[9];

    public LevelDifficult LevelDifficulty => levelDifficulty;
    [SerializeField]
    private LevelDifficult levelDifficulty;

    public int MaxMistakes => maxMisatkes;
    [SerializeField]
    private int maxMisatkes = 3;

    public enum LevelDifficult
    {
        EASY = 0,
        MEDIUM,
        HARD
    }

    #region PublicFunctions
    public void SetLevelDifficulty(LevelDifficult newDifficulty)
    {
        levelDifficulty = newDifficulty;

        HideNumbersByDifficulty();
    }

    public void HideNumbersByDifficulty()
    {
        numberHide = (string[]) numberSolution.Clone();

        // Hides numbers
        switch (levelDifficulty)
        {
            case LevelDifficult.EASY:
                RemoveDigits(40);
                break;
            case LevelDifficult.MEDIUM:
                RemoveDigits(50);
                break;
            case LevelDifficult.HARD:
                RemoveDigits(60);
                break;
            default:
                break;
        }
    }

    public bool CheckIfLevelValid()
    {
        // Check Solution number
        for (int i = 0; i < numberSolution.Length; i++)
        {
            if (numberSolution[i].Length != 9)
            {
                Debug.LogError($"Level no valid! Row {i} has {numberSolution[i].Length} size!");
                return false;
            }
        }

        // Check Hide numbers
        for (int i = 0; i < numberHide.Length; i++)
        {
            if (numberHide[i].Length != 9)
            {
                Debug.LogError($"Level no valid! Row {i} has {numberHide[i].Length} size!");
                return false;
            }
        }

        return true;
    }
    #endregion

    #region PrivateFunctions
    private void RemoveDigits(int digits)
    {
        int remainingDigits = digits;
        int maxTries = 200;
        int tries = 0;

        while(remainingDigits > 0 && tries < maxTries)
        {
            int randX = Random.Range(0, 9);
            int randY = Random.Range(0, 9);

            if (GetNumberByPosition(randX, randY) != '-')
            {
                char character = numberHide[randX][randY];
                numberHide[randX] = numberHide[randX].Replace(character, '-');
                remainingDigits--;
            }
            else
                tries++;
        }
    }

    private char GetNumberByPosition(int i, int j)
    {
        return numberHide[i][j];
    }
    #endregion
}
