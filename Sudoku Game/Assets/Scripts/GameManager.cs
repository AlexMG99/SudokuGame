using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public enum GameStatus
    {
        PLAY,
        PAUSE,
        END
    }

    [Header("Pause UI")]
    [SerializeField]
    private Image pauseButtonImage;
    [SerializeField]
    private Sprite playIcon;
    [SerializeField]
    private Sprite pauseIcon;

    public GameStatus GameState => gameState;
    private GameStatus gameState = GameStatus.PLAY;

    #region PublicFunction
    public void WinGame()
    {
        // UI Win appear
        gameState = GameStatus.END;
    }

    public void LoseGame()
    {
        // UI Lose appear
        gameState = GameStatus.END;
    }

    public void PauseUnpauseGame()
    {
        if (gameState == GameStatus.PLAY)
        {
            gameState = GameStatus.PAUSE;
            pauseButtonImage.sprite = playIcon;
        }
        else if (gameState == GameStatus.PAUSE)
        {
            gameState = GameStatus.PLAY;
            pauseButtonImage.sprite = pauseIcon;
        }
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion

}
