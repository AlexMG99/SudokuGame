using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

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

    [Header("UI elements")]
    [SerializeField] private TextMeshProUGUI levelDifficultyText;
    [SerializeField] private TextMeshProUGUI mistakesText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameplayTimeText;

    private int levelScore = 0;
    private int mistakes = 0;
    private int maxMistakes = 0;

    // Time
    private float timeElapsed = 0f;
    private int minutes, seconds;

    #region MonoBehaviourFunctions
    void Start()
    {
        Init();
    }

    void Update()
    {
        if (GameManager.Instance.GameState != GameManager.GameStatus.PLAY)
            return;

        timeElapsed += Time.deltaTime;
        minutes = (int)(timeElapsed / 60f);
        seconds = (int)(timeElapsed - minutes * 60f);

        gameplayTimeText.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }

    #endregion

    #region PrivateFunctions
    private void Init()
    {
        // Level Text
        string levelDifficultyString = "None";
        switch (currentLevel.LevelDifficulty)
        {
            case SudokuLevelSO.LevelDifficult.EASY:
                levelDifficultyString = "Easy";
                break;
            case SudokuLevelSO.LevelDifficult.MEDIUM:
                levelDifficultyString = "Medium";
                break;
            case SudokuLevelSO.LevelDifficult.HARD:
                levelDifficultyString = "Hard";
                break;
            default:
                break;
        }

        levelDifficultyText.text = levelDifficultyString;

        // Mistakes
        maxMistakes = currentLevel.MaxMistakes;
        mistakesText.text = $"Mistakes {mistakes}/{maxMistakes}";

        // Score
        scoreText.text = levelScore.ToString();

    }

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

    public void ResetLevel()
    {
        // Set level values to 0
        timeElapsed = 0f;

        levelScore = 0;
        scoreText.text = levelScore.ToString();

        mistakes = 0;
        mistakesText.text = mistakesText.text = $"Mistakes {mistakes}/{maxMistakes}";

        GridController.Instance.ResetLevel();
        InputController.Instance.ClearQueue();
    }

    public void LoadNextLevel()
    {
        PlayerPrefs.SetInt("LevelIndex", currentLevelIdx + 1);

        LoadLevel();
        GridController.Instance.ResetLevel();
    }

    public void AddMistake()
    {
        if (GameManager.Instance.GameState != GameManager.GameStatus.PLAY)
            return;

        mistakes++;
        mistakesText.text = $"Mistakes {mistakes}/{maxMistakes}";

        if (mistakes >= maxMistakes)
            GameManager.Instance.LoseGame();
    }

    public void AddScore(int score)
    {
        if (GameManager.Instance.GameState != GameManager.GameStatus.PLAY)
            return;

        levelScore += score;
        scoreText.text = levelScore.ToString();
    }
    #endregion
}
