using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [Header("Levels Sudoku")]
    [SerializeField]
    private List<SudokuLevelSO> levels;

    [SerializeField]
    private SudokuLevelSO testLevel;
    [SerializeField]
    private bool isTestLevel = false;

    public SudokuLevelSO CurrentLevel => currentLevel;
    private SudokuLevelSO currentLevel;
    private int currentLevelIdx = 0;

    #region MonoBehaviourFunctions
    void Awake()
    {

    }

    #endregion

    #region PrivateFunctions

    #endregion

    #region PublicFunctions
    public void LoadLevel()
    {
        if (testLevel || levels.Count == 0)
            currentLevel = testLevel;
        else
        {
            currentLevelIdx = PlayerPrefs.GetInt("LevelIndex", 0);
            currentLevel = levels[currentLevelIdx];
        }
    }
    #endregion
}