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
    public void HideNumbersByDifficulty()
    {
        // Hides numbers
        switch (levelDifficulty)
        {
            case LevelDifficult.EASY:
                break;
            case LevelDifficult.MEDIUM:
                break;
            case LevelDifficult.HARD:
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
}
