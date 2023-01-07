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

    //private int gameplayTime = 0f;

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
        if (isTestLevel || levels.Count == 0)
            currentLevel = testLevel;
        else
        {
            currentLevelIdx = PlayerPrefs.GetInt("LevelIndex", 0);
            currentLevel = levels[currentLevelIdx];
        }

        if (!currentLevel.CheckIfLevelValid())
            Debug.LogError("Level No Valid!");
    }
    #endregion
}
