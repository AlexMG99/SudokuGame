using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Audio.AudioSFX;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public enum GameStatus
    {
        PLAY,
        PAUSE,
        END
    }

    [Header("End Screen UI")]
    [SerializeField]
    private GameObject winScreen;
    [SerializeField]
    private GameObject loseScreen;

    [Header("Pause UI")]
    [SerializeField]
    private Image pauseButtonImage;
    [SerializeField]
    private Sprite playIcon;
    [SerializeField]
    private Sprite pauseIcon;
    [SerializeField]
    private GameObject pauseScreen;

    [SerializeField]
    private Image backButtonImage;

    public GameStatus GameState => gameState;
    private GameStatus gameState = GameStatus.PLAY;

    #region MonoBehaviourFunction
    private void Start()
    {
        SetSkin();
    }
    #endregion

    #region PublicFunction
    public void SetSkin()
    {
        pauseButtonImage.color = backButtonImage.color = SkinController.Instance.CurrentGridSkin.ButtonUIColor;
    }

    public void WinGame()
    {
        // UI Win appear
        gameState = GameStatus.END;

        winScreen.SetActive(true);
    }

    public void LoseGame()
    {
        // UI Lose appear
        gameState = GameStatus.END;

        loseScreen.SetActive(true);
    }

    public void PauseUnpauseGame()
    {
        if (gameState == GameStatus.PLAY)
        {
            gameState = GameStatus.PAUSE;
            pauseButtonImage.sprite = playIcon;
            pauseScreen.SetActive(true);
        }
        else if (gameState == GameStatus.PAUSE)
        {
            gameState = GameStatus.PLAY;
            pauseButtonImage.sprite = pauseIcon;
            pauseScreen.SetActive(false);
        }

        AudioSFX.Instance.PlaySFX("UIButton");
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ContinueLostLevel()
    {
        // See ad
        loseScreen.SetActive(false);

        gameState = GameStatus.PLAY;
        GridController.Instance.LevelController.ResetLevel();
    }

    public void NextLevel()
    {
        winScreen.SetActive(false);

        gameState = GameStatus.PLAY;
        GridController.Instance.LevelController.LoadNextLevel();
    }
    #endregion


}
