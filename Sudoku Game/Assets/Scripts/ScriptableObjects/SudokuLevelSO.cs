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

    private LevelDifficulty LevelDifficulty1 => levelDifficulty;
    [SerializeField]
    private LevelDifficulty levelDifficulty;
    
    enum LevelDifficulty
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
            case LevelDifficulty.EASY:
                break;
            case LevelDifficulty.MEDIUM:
                break;
            case LevelDifficulty.HARD:
                break;
            default:
                break;
        }
    }
    #endregion
}
